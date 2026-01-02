# Users API #

**Base path:** `/api/users`  
**Method:** `POST`  
**Header:** `Content-Type: application/json`

> ℹ **INFO**  
> Some fields are **IDs from the `offers` database dictionaries** (you send IDs only). To resolve IDs → labels/names, use the **offers API**.

> ℹ **INFO**  
> Some nested response objects may use `snake_case` keys (as returned by backend). Do not rename keys on the client side.

---

## POST /api/users/standard-register ##

**Purpose:** Register with email + password.

### Body parameters ###
- `email` (string, required)
- `password` (string, required)
- `firstName` (string, required)
- `lastName` (string, required)

### Example request ###
```json
{
    "email": "user01@a.pl",
    "password": "VeryStrongPassword123!",
    "firstName": "User01",
    "lastName": "Test"
}
```

### Possible responses ###
- **201 Created**
```json
{
    "errorMessage": ""
}
```

- **400 Bad Request** / **409 Conflict** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>"
}
```

---

## POST /api/users/standard-login ##

**Purpose:** Login with email + password. Optionally returns a `rememberMeToken`.

### Body parameters ###
- `login` (string, required; email)
- `password` (string, required)
- `rememberMe` (boolean, required)

### Example request ###
```json
{
    "login": "user01@a.pl",
    "password": "VeryStrongPassword123!",
    "rememberMe": true
}
```

### Possible responses ###
- **200 OK**
```json
{
    "errorMessage": "",
    "jwt": "<jwt>",
    "rememberMeToken": "<token or empty string>"
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>",
    "jwt": "",
    "rememberMeToken": ""
}
```

---

## POST /api/users/token-login ##

**Purpose:** Login using `rememberMeToken` obtained from `standard-login`.

### Body parameters ###
- `token` (string, required)

### Example request ###
```json
{
    "token": "<rememberMeToken>"
}
```

### Possible responses ###
- **200 OK**
```json
{
    "errorMessage": "",
    "jwt": "<jwt>",
    "rememberMeToken": "<new token>"
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>",
    "jwt": "",
    "rememberMeToken": ""
}
```

---

## POST /api/users/update-data ##

**Purpose:** Update user profile fields (partial update).

### Body parameters ###
- `jwt` (string, required)
- `userFirstName` (string, optional)
- `userSecondName` (string, optional)
- `userLastName` (string, optional)
- `userPhone` (string, optional; e.g. `"+48111111111"`)

> ⚠ **WARNING**  
> You must send at least one field to update (besides `jwt`). Otherwise the endpoint returns **400**.

### Example request ###
```json
{
    "jwt": "<jwt>",
    "userPhone": "+48111111111",
    "userSecondName": "Jan"
}
```

### Possible responses ###
- **200 OK**
```json
{
    "errorMessage": "",
    "userFirstNameUpdated": false,
    "userSecondNameUpdated": true,
    "userLastNameUpdated": false,
    "userPhoneUpdated": true
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**  
(same structure; update flags typically `false`)
```json
{
    "errorMessage": "<non-empty string>",
    "userFirstNameUpdated": false,
    "userSecondNameUpdated": false,
    "userLastNameUpdated": false,
    "userPhoneUpdated": false
}
```

---

## POST /api/users/update-weights ##

**Purpose:** Update ranking-algorithm weights (partial update).

### Body parameters ###
- `jwt` (string, required)
- Any subset below (optional; arrays must be non-empty to update):
    - `vector` (number[])
    - `meanDist` (number[])
    - `meanValueIds` (string[])
    - `orderByOption` (string[])
    - `meansValueSum` (number[])
    - `meansValueSsum` (number[])
    - `meansValueCount` (number[])
    - `meansWeightSum` (number[])
    - `meansWeightSsum` (number[])
    - `meansWeightCount` (number[])

> ⚠ **WARNING**  
> You must provide at least one non-empty array (besides `jwt`). Otherwise the endpoint returns **400**.

### Example request ###
```json
{
    "jwt": "<jwt>",
    "vector": [
        0.5,
        0.5,
        0.5,
        0.5,
        0.5,
        0.5,
        0.5,
        0.5
    ]
}
```

### Possible responses ###
- **200 OK**
```json
{
    "errorMessage": "",
    "orderByOptionUpdated": false,
    "meanValueIdsUpdated": false,
    "vectorUpdated": true,
    "meanDistUpdated": false,
    "meansValueSumUpdated": false,
    "meansValueSsumUpdated": false,
    "meansValueCountUpdated": false,
    "meansWeightSumUpdated": false,
    "meansWeightSsumUpdated": false,
    "meansWeightCountUpdated": false
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**  
(same structure)
```json
{
    "errorMessage": "<non-empty string>",
    "orderByOptionUpdated": false,
    "meanValueIdsUpdated": false,
    "vectorUpdated": false,
    "meanDistUpdated": false,
    "meansValueSumUpdated": false,
    "meansValueSsumUpdated": false,
    "meansValueCountUpdated": false,
    "meansWeightSumUpdated": false,
    "meansWeightSsumUpdated": false,
    "meansWeightCountUpdated": false
}
```

---

## POST /api/users/change-password ##

**Purpose:** Change password.

### Body parameters ###
- `jwt` (string, required)
- `newPassword` (string, required)

### Example request ###
```json
{
    "jwt": "<jwt>",
    "newPassword": "NewStrongPassword123!"
}
```

### Possible responses ###
- **200 OK**
```json
{
    "errorMessage": ""
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>"
}
```

---

## POST /api/users/logout ##

**Purpose:** Logout user.

### Body parameters ###
- `jwt` (string, required)

### Example request ###
```json
{
    "jwt": "<jwt>"
}
```

### Possible responses ###
- **200 OK**
```json
{
    "errorMessage": ""
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>"
}
```

---

## POST /api/users/check-permission ##

**Purpose:** Check if user has a permission.

### Body parameters ###
- `jwt` (string, required)
- `permissionName` (string, required)

### Example request ###
```json
{
    "jwt": "<jwt>",
    "permissionName": "admin_panel_access"
}
```

### Possible responses ###
- **200 OK** (has permission)
```json
{
    "errorMessage": ""
}
```

- **403 Forbidden** (no permission)
```json
{
    "errorMessage": "<non-empty string>"
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>"
}
```

---

## POST /api/users/insert-search-history ##

**Purpose:** Store a search snapshot.

### Body parameters ###
- `jwt` (string, required)
- Optional (send any subset; at least one must be present):
    - `keywords` (string)
    - `distance` (number)
    - `isRemote` (boolean)
    - `isHybrid` (boolean)
    - `leadingCategoryId` (number; offers dictionary id)
    - `cityId` (number; offers dictionary id)
    - `salaryFrom` (number)
    - `salaryTo` (number)
    - `salaryPeriodId` (number; offers dictionary id)
    - `salaryCurrencyId` (number; offers dictionary id)
    - `employmentScheduleIds` (number[]; offers dictionary ids)
    - `employmentTypeIds` (number[]; offers dictionary ids)

> ⚠ **WARNING**  
> If you send only `jwt` (no search fields), the endpoint returns **400**.

### Example request ###
```json
{
    "jwt": "<jwt>",
    "keywords": "DevOps AWS",
    "distance": 40,
    "isRemote": true,
    "isHybrid": false,
    "salaryFrom": 13000.00,
    "salaryTo": 22000.00,
    "salaryPeriodId": 4,
    "salaryCurrencyId": 4,
    "leadingCategoryId": 40,
    "cityId": 1,
    "employmentTypeIds": [
        5
    ],
    "employmentScheduleIds": [
        1
    ]
}
```

### Possible responses ###
- **200 OK**
```json
{
    "errorMessage": ""
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>"
}
```

---

## POST /api/users/delete-user ##

**Purpose:** Delete user account.

### Body parameters ###
- `jwt` (string, required)

### Example request ###
```json
{
    "jwt": "<jwt>"
}
```

### Possible responses ###
- **200 OK**
```json
{
    "errorMessage": ""
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>"
}
```

---

## POST /api/users/refresh-jwt ##

**Purpose:** Get a new JWT.

### Body parameters ###
- `jwt` (string, required)

### Example request ###
```json
{
    "jwt": "<jwt>"
}
```

### Possible responses ###
- **200 OK**
```json
{
    "errorMessage": "",
    "jwt": "<new jwt>"
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>",
    "jwt": ""
}
```

---

## POST /api/users/check-jwt ##

**Purpose:** Validate JWT (useful for checking if user session is still valid).

### Body parameters ###
- `jwt` (string, required)

### Example request ###
```json
{
    "jwt": "<jwt>"
}
```

### Possible responses ###
- **200 OK**
```json
{
    "result": true
}
```

- **400 Bad Request** / **401 Unauthorized**
```json
{
    "result": false
}
```

---

## POST /api/users/get-search-history ##

**Purpose:** Fetch last N searches.

### Body parameters ###
- `jwt` (string, required)
- `limit` (number, required; must be > 0)

> ⚠ **WARNING**  
> `limit` MUST be provided, otherwise you should expect an empty result.

### Example request ###
```json
{
    "jwt": "<jwt>",
    "limit": 2
}
```

### Possible responses ###
- **200 OK** (example)
```json
{
    "errorMessage": "",
    "searchHistory": [
        {
            "city_id": 1,
            "user_id": 1,
            "distance": 40,
            "keywords": "DevOps AWS",
            "is_hybrid": false,
            "is_remote": true,
            "salary_to": 22000.00,
            "salary_from": 13000.00,
            "salary_period_id": 4,
            "salary_currency_id": 4,
            "employment_type_ids": [
                5
            ],
            "leading_category_id": 40,
            "employment_schedule_ids": [
                1
            ]
        },
        {
            "city_id": 1,
            "user_id": 1,
            "distance": 50,
            "keywords": "Inżynier AI ML",
            "is_hybrid": true,
            "is_remote": true,
            "salary_to": 24000.00,
            "salary_from": 14000.00,
            "salary_period_id": 4,
            "salary_currency_id": 1,
            "employment_type_ids": [
                5
            ],
            "leading_category_id": 35,
            "employment_schedule_ids": [
                1,
                10
            ]
        }
    ]
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>",
    "searchHistory": []
}
```

---

## POST /api/users/get-weights ##

**Purpose:** Fetch stored weights.

### Body parameters ###
- `jwt` (string, required)

### Example request ###
```json
{
    "jwt": "<jwt>"
}
```

### Possible responses ###
- **200 OK** (example)
```json
{
    "errorMessage": "",
    "weights": {
        "vector": [
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5,
            0.5
        ],
        "mean_dist": [
            0.5
        ],
        "mean_value_ids": [
            ""
        ],
        "means_value_sum": [
            0
        ],
        "order_by_option": [
            ""
        ],
        "means_value_ssum": [
            0
        ],
        "means_weight_sum": [
            0
        ],
        "means_value_count": [
            0
        ],
        "means_weight_ssum": [
            0
        ],
        "means_weight_count": [
            0
        ]
    }
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>",
    "weights": {}
}
```

---

## POST /api/users/get-data ##

**Purpose:** Fetch user profile data (some fields may be `null`).

### Body parameters ###
- `jwt` (string, required)

### Example request ###
```json
{
    "jwt": "<jwt>"
}
```

### Possible responses ###
- **200 OK** (example)
```json
{
    "errorMessage": "",
    "userData": {
        "role": "User",
        "email": "user01@a.pl",
        "phone": "+48111111111",
        "last_name": "Test",
        "first_name": "User01",
        "second_name": "Jan"
    }
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>",
    "userData": {}
}
```

---

## POST /api/users/update-preferences ##

**Purpose:** Update personal offer preferences (partial update).

### Body parameters ###
- `jwt` (string, required)
- Optional fields:
    - `leadingCategoryId` (number; offers dictionary id)
    - `salaryFrom` (number)
    - `salaryTo` (number)
    - `employmentTypeIds` (number[]; offers dictionary ids)
    - `languageId` (number; offers dictionary id)
    - `languageLevelId` (number; offers dictionary id)
    - `jobStatusName` (string)
    - `cityName` (string)
    - `workTypeNames` (string[])
    - `skillNames` (string[])
    - `skillMonths` (number[])

> ⚠ **WARNING**  
> For language update: send BOTH `languageId` and `languageLevelId`.

> ⚠ **WARNING**  
> For skills update: send BOTH arrays `skillNames` + `skillMonths` and they must have the same length.

### Example request (category + language) ###
```json
{
    "jwt": "<jwt>",
    "leadingCategoryId": 3,
    "languageId": 3,
    "languageLevelId": 5
}
```

### Possible responses ###
- **200 OK**
```json
{
    "errorMessage": "",
    "leadingCategoryIdUpdated": true,
    "salaryFromUpdated": false,
    "salaryToUpdated": false,
    "employmentTypeIdsUpdated": false,
    "languageIdUpdated": true,
    "languageLevelIdUpdated": true,
    "jobStatusNameUpdated": false,
    "cityNameUpdated": false,
    "workTypeNamesUpdated": false,
    "skillNamesUpdated": false,
    "skillMonthsUpdated": false
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**  
(same structure; update flags typically `false`)
```json
{
    "errorMessage": "<non-empty string>",
    "leadingCategoryIdUpdated": false,
    "salaryFromUpdated": false,
    "salaryToUpdated": false,
    "employmentTypeIdsUpdated": false,
    "languageIdUpdated": false,
    "languageLevelIdUpdated": false,
    "jobStatusNameUpdated": false,
    "cityNameUpdated": false,
    "workTypeNamesUpdated": false,
    "skillNamesUpdated": false,
    "skillMonthsUpdated": false
}
```

---

## POST /api/users/get-preferences ##

**Purpose:** Get saved preferences formatted as JSON.  
If preferences are incomplete, the endpoint returns **422** with a payload.

### Body parameters ###
- `jwt` (string, required)

### Example request ###
```json
{
    "jwt": "<jwt>"
}
```

### Possible responses ###
- **200 OK** (complete; example)
```json
{
    "errorMessage": "",
    "preferences": {
        "leading_category_id": 3,
        "salary_from": 12000.00,
        "salary_to": 18000.00,
        "employment_type_ids": [
            1,
            3
        ],
        "job_status": "actively_looking",
        "city_name": "Warszawa",
        "work_types": [
            "hybrid",
            "remote"
        ],
        "languages": [
            {
                "language_id": 3,
                "language_level_id": 5
            },
            {
                "language_id": 1,
                "language_level_id": 4
            }
        ],
        "skills": [
            {
                "skill_name": "C#",
                "experience_months": 24,
                "entry_date": "2026-01-01T01:11:53.964356Z"
            },
            {
                "skill_name": "Docker",
                "experience_months": 8,
                "entry_date": "2026-01-01T01:11:53.964356Z"
            }
        ]
    }
}
```

- **422 Unprocessable Entity** (incomplete; example)
```json
{
    "errorMessage": "Preferences are incomplete",
    "preferences": {
        "leading_category_id": 3,
        "employment_type_ids": [],
        "work_types": [],
        "languages": [
            {
                "language_id": 3,
                "language_level_id": 5
            }
        ],
        "skills": []
    }
}
```

- **400 Bad Request** / **401 Unauthorized** / **500 Internal Server Error**
```json
{
    "errorMessage": "<non-empty string>",
    "preferences": {}
}
```
