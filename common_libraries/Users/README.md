# This readme was created using deprecated version of Users library. #

# Usage of Users #
## Add Users to project ##
> ℹ **info** This library is dependent on: \
>NuGet packages:
>- HtmlSanitizer(9.0.886) by Michael Ganss
>- Npgsql(9.0.3) by Shay Rojansky,Nikita Kazmin,Brar Piening,Nino Floris,Yoh Deadfall,Austin Drenski,Emil Lenngren,Francisco Figueiredo Jr.,Kenji Uno
>- Konscious.Security.Cryptography.Argon2(1.3.1) by Keef Aragon
>- Microsoft.IdentityModel.Tokens(8.15.0) by Microsoft
>- System.IdentityModel.Tokens.Jwt(8.15.0) by Microsoft

1. Right-click on `Dependencies` -> `add` -> `Project reference`.
2. `Browse` on the bottom panel of `Reference manager`.
3. Select `Users.dll` and click `Add`.
4. Import library inside the code
```c#
using Users;
```

## Creating shared objects ##
> ⚠ **warning** Creating those objects should occur only one time, and they should be shared between all user objects

```c#
// Shared database object
await using NpgsqlDataSource dataSource = UserUtils.CreateDataSource("username", "password", "db_container_name");

// Default password policy
UserPasswordPolicy passwordPolicy = new();
```

### UserPasswordPolicy ###
> ℹ **info** `UserPasswordPolicy` is a structure containing password requirements.
Additionally this structure allows for specifying a file containing disallowed passwords (one per line in unencrypted form).

#### Parameters ####
- **minLength**: *int* - minimum allowed password length. Defaults to **12**.
- **maxLength**: *int* - maximum allowed password length (hard limited to 512). Defaults to **128**.
- **requireUppercase**: *bool* - whether at least one uppercase letter is required. Defaults to **false**.
- **requireLowercase**: *bool* - whether at least one lowercase letter is required. Defaults to **false**.
- **requireDigit**: *bool* - whether at least one digit is required. Defaults to **false**.
- **requireNonAlphanumeric**: *bool* - whether at least one special character is required. Defaults to **false**.
- **requiredUniqueChars**: *int* - minimum number of distinct characters required. Defaults to **4**.
- **allowedSpecialCharacters**: *string?* - allowed special characters when validating non-alphanumeric characters.  Defaults to **!@#$%^&*()-_=+[]{};:,.<>?**.
- **knownPasswordsListPath**: *string?* - path to disallowed passwords file. Defaults to **null**.

#### Code example ####
```c#
// New object with default values
UserPasswordPolicy passwordPolicy = new();

// Object with custom properties
UserPasswordPolicy passwordPolicy = new(16, 64, true, true, true, true, null, "path_to_password_list.txt");
```

#### Verifying passwords ####
```c#
/* UserPasswordPolicy structure creation */

string password = "1qazXSW@";
(bool isValid, string error) = passwordPolicy.ValidatePassword(password);
if (!isValid) // On success error is empty
    Console.WriteLine(error);
```

## User ##
> ⚠ **WARNING** ALL MESSAGES RETURNED BY USER CLASS, EITHER BY EXCEPTIONS OR RETURNS ARE FOR LOGGING PURPOSES ONLY AND SHOULD NOT BE SHOWN TO THE USER.

> ℹ **info** This class represents a user and is bound to a single entity. Until successful login or registration, object remains **ownerless** and might be reused.

> ⚠ **warning** Trying to assign a new user to **owned** user object will result in a exception.

### Object creation ###
```c#
/* NpgsqlDataSource object creation */

// Now object is in a ownerless state, and is ready for user assignment
User user = new(datasource);
```
### Check object owner ###
To check if object is **owned** or **ownerless** you may use `IsLoggedIn` function.

```c#
/* User object creation */

bool result = user.IsLoggedIn();
```

### New user registration ###
> ℹ **info** After successful registration object is **owned** and user is already **logged in**. Unsuccessful registration leaves object in a **ownerless** state, and might be freely used.

```c#
/* User object creation */

// All values are required and must not be empty
(bool result, string error) = await user.StandardRegisterAsync(
    passwordPolicy,
    "email",
    "password",
    "first_name",
    "last_name");
```

### User log in ###
> ℹ **info** After successful log in object is **owned**. Unsuccessful log in leaves object in a **ownerless** state, and might be freely used.

#### Password log in ####
```c#
/* User object creation */

(bool result, string error, string? token) = await user.StandardAuthAsync(
    "email",
    "password",
    true // Generate remember me token
);

if (result)
    if (token is not null) // Even on success token might be null, if its creation failed
        Console.WriteLine(token); // Token should be passed to the user, saved and used to log in next time
```

#### Token log in ####
> ℹ **info** This function generates new token for every successful login, new token should be passed to the user.

```c#
/* User object creation */

(bool result, string error, string? token) = await user.AuthWithTokenAsync(
    "user_token"
);

if (result)
    if (token is not null) // Even on success token might be null, if its creation failed
        Console.WriteLine(token); // Token should be passed to the user, saved and used to log in next time
```

### Algorithm weights manipulation ###

#### Weights retrieval ####
> ℹ **info** Weights are returned as json string (empty json on failure). Field names corresponds to their names in database.

```c#
/* User object creation and successful log in*/

string weights = await user.GetWeightsAsync();
```

#### Weights manipulation ####
> ℹ **info** `UpdateWeightsAsync` returns per field result (if the field was changed or not).
```c#
/* User object creation and successful log in*/

// Input dictionary preparation, dict values should correspond to declared data types in a data base.
// Incorrect data types will be discarded and returned as false.
Dictionary<string, object> fieldValues = new() {
    {"means_weight_sum", [1.0, 2.3, 5.0]},
    {"means_value_count", [1, 2, 3, 4]}
};

// Dictionary with a per field result
Dictionary<bool, string> results = await user.UpdateWeightsAsync(fieldValues);
```

### User data manipulation ###

#### User data retrieval ####
> ℹ **info** User data is returned as json string (empty json on failure). Field names corresponds to their names in database. **[...]_id** fields have **_id** removed from field name. This function returns **only** public data without hashes

```c#
/* User object creation and successful log in*/

string data = await user.GetDataAsync();
```

#### Data manipulation ####
> ℹ **info** `UpdateDataAsync` returns per field result (if the field was changed or not).

> ℹ **info** Currently fieldValues keys are: "user_first_name", "user_second_name", "user_last_name", "user_phone".
```c#
/* User object creation and successful log in*/

Dictionary<string, string> fieldValues = new() {
    {"user_first_name", "Jan"},
    {"user_last_name", "Kowalski"}
};

// Dictionary with a per field result
Dictionary<bool, string> results = await user.UpdateDataAsync(fieldValues);
```

#### Changing password ####
> ℹ **info** To avoid mistakes changing password is a separate function.

> ℹ **info** This function invalidates user remember me token.

```c#
/* User object creation and successful log in*/

// New password must be compliant to the password policy
bool result = await user.ChangePasswordAsync(passwordPolicy, "1qazXSW@");
```

### User manipulation ###

#### Logout ####
> ⚠ **warning** This function runs dispose on the object.

> ℹ **info** This function invalidates user remember me token.

```c#
/* User object creation and successful log in*/

await user.LogoutAsync();
```

#### Deleting user ####
> ⚠ **warning** This function runs dispose on the object.


```c#
/* User object creation and successful log in*/

bool result = await user.DeleteUserAsync();
```

### Checking permissions ###
> ℹ **info** `CheckPermissionAsync` returns false when an error occurs, when the permission
is null/whitespace, or when the given permission does not exist in the database or
is not assigned to the user.

```c#
/* User object creation and successful log in*/

bool canEditArticles = await user.CheckPermissionAsync("CAN_EDIT_ARTICLES");
```

### Exception handling ###
This class throws exceptions:
- UserException
- UserDbQueryException
- UserCryptographicException
- OperationCancelledException

For details on thrown exceptions please refer to the in-code function descriptions.