using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using offer_manager.Models.Users;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Users;

namespace offer_manager.Controllers
{
    /// <summary>
    /// Exposes user-related endpoints under <c>/api/users</c> (JSON over HTTP).
    /// Authenticated operations expect a JWT passed in the request body.
    /// For request/response schemas and examples, see <c>UserAPI-DOCS.md</c>.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public partial class UserController(User userController, UserPasswordPolicy userPasswordPolicy, JwtOptions jwtOptions) : ControllerBase
    {
        /// <summary>
        /// Regular expression used to validate email addresses in registration and login requests.
        /// </summary>
        private static readonly Regex _emailRegex = new(
            @"^(?=.{1,254}$)(?=.{1,64}@)[A-Za-z0-9]+([._%+\-]?[A-Za-z0-9]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9\-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,63}$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant
        );

        /// <summary>
        /// Regular expression used to validate phone numbers in international format (e.g. <c>+48111111111</c>).
        /// </summary>
        private static readonly Regex _phoneRegex = new(
            @"^\+[0-9]{7,15}$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant
        );

        /// <summary>
        /// Database column name constants used by partial-update endpoints (e.g. update-data / update-weights / update-preferences).
        /// </summary>
        private const string _fieldUserFirstName = "user_first_name";
        private const string _fieldUserSecondName = "user_second_name";
        private const string _fieldUserLastName = "user_last_name";
        private const string _fieldUserPhone = "user_phone";

        private const string _fieldOrderByOption = "order_by_option";
        private const string _fieldMeanValueIds = "mean_value_ids";
        private const string _fieldVector = "vector";
        private const string _fieldMeanDist = "mean_dist";
        private const string _fieldMeansValueSum = "means_value_sum";
        private const string _fieldMeansValueSsum = "means_value_ssum";
        private const string _fieldMeansValueCount = "means_value_count";
        private const string _fieldMeansWeightSum = "means_weight_sum";
        private const string _fieldMeansWeightSsum = "means_weight_ssum";
        private const string _fieldMeansWeightCount = "means_weight_count";

        private const string _fieldLeadingCategory = "leading_category";
        private const string _fieldSalaryRange = "salary_range";
        private const string _fieldEmploymentTypes = "employment_types";
        private const string _fieldLanguagePair = "language_pair";
        private const string _fieldJobStatus = "job_status";
        private const string _fieldCity = "city";
        private const string _fieldWorkTypes = "work_types";
        private const string _fieldSkills = "skills";

        /// <summary>
        /// Common, localized error message texts returned in response payloads across endpoints.
        /// </summary>
        private const string _errEmptyRequest = "Puste żądanie.";
        private const string _errLoginRequired = "Wymagane logowanie przed wykonaniem tej akcji.";
        private const string _errNothingToUpdate = "Brak danych do aktualizacji.";
        private const string _errInvalidEmail = "Niepoprawny format adresu e-mail.";
        private const string _errJwtExpired = "Jwt wygasł.";
        private const string _errJwtInvalid = "Niepoprawny jwt.";
        private const string _errInternalServer = "Wewnętrzny błąd serwera.";

        /// <summary>
        /// User service responsible for database-backed operations on user accounts and user-related data.
        /// </summary>
        private readonly User _userController = userController;

        /// <summary>
        /// Password complexity policy used when registering users and changing passwords.
        /// </summary>
        private readonly UserPasswordPolicy _userPasswordPolicy = userPasswordPolicy;

        /// <summary>
        /// JWT configuration (e.g. issuer, secret, validation parameters) used to generate and validate tokens.
        /// </summary>
        private readonly JwtOptions _jwtOptions = jwtOptions;

        /// <summary>
        /// Registers a new user using email and password credentials.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing email, password, first name, and last name.</param>
        /// <returns>
        /// 201 on success; otherwise an error response with a non-empty <c>errorMessage</c>.
        /// </returns>
        [HttpPost("standard-register")]
        public async Task<ActionResult<StandardRegisterUserResponse>> StandardRegister(
            [FromBody] StandardRegisterUserRequest? request
        )
        {
            if (request is null)
                return BadRequest(new StandardRegisterUserResponse { ErrorMessage = _errEmptyRequest });

            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName))
            {
                return BadRequest(new StandardRegisterUserResponse { ErrorMessage = "Uzupełnij wszystkie wymagane pola i spróbuj ponownie." });
            }

            if (!_emailRegex.IsMatch(request.Email))
                return BadRequest(new StandardRegisterUserResponse { ErrorMessage = _errInvalidEmail });

            if (!_userPasswordPolicy.ValidatePassword(request.Password).IsValid)
                return BadRequest(new StandardRegisterUserResponse { ErrorMessage = "Hasło nie spełnia wymagań bezpieczeństwa." });

            try
            {
                (bool result, string error) = await _userController.StandardRegisterAsync(_userPasswordPolicy, request.Email, request.Password, request.FirstName, request.LastName);
                if (!result)
                    return Conflict(new StandardRegisterUserResponse
                    {
                        ErrorMessage = "Ten e-mail jest już zajęty."
                    });

                return StatusCode(StatusCodes.Status201Created, new StandardRegisterUserResponse { ErrorMessage = string.Empty });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new StandardRegisterUserResponse { ErrorMessage = _errInternalServer });
            }
        }

        /// <summary>
        /// Authenticates a user using email (login) and password.
        /// On success returns a JWT and (optionally) a remember-me token.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing login (email), password, and remember-me flag.</param>
        /// <returns>
        /// 200 with J
        [HttpPost("standard-login")]
        public async Task<ActionResult<StandardLoginResponse>> StandardLogin(
            [FromBody] StandardLoginRequest? request
        )
        {
            if (request is null)
            {
                return BadRequest(new StandardLoginResponse
                {
                    ErrorMessage = _errEmptyRequest,
                    Jwt = string.Empty,
                    RememberMeToken = string.Empty
                });
            }

            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new StandardLoginResponse
                {
                    ErrorMessage = "Uzupełnij login i hasło.",
                    Jwt = string.Empty,
                    RememberMeToken = string.Empty
                });
            }

            if (!_emailRegex.IsMatch(request.Login))
            {
                return BadRequest(new StandardLoginResponse
                {
                    ErrorMessage = _errInvalidEmail,
                    Jwt = string.Empty,
                    RememberMeToken = string.Empty
                });
            }

            try
            {
                (bool result, string error, string? rememberToken, long? userId) = await _userController.StandardAuthAsync(request.Login, request.Password, request.RememberMe);
                if (!result)
                    return Unauthorized(new StandardLoginResponse
                    {
                        ErrorMessage = "Niepoprawny login lub hasło.",
                        Jwt = string.Empty,
                        RememberMeToken = string.Empty
                    });

                if (userId is null)
                    throw new Exception("Empty userId");

                return Ok(new StandardLoginResponse
                {
                    ErrorMessage = string.Empty,
                    Jwt = JwtUtils.Generate(_jwtOptions, userId.Value),
                    RememberMeToken = rememberToken ?? string.Empty
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new StandardLoginResponse
                {
                    ErrorMessage = _errInternalServer,
                    Jwt = string.Empty,
                    RememberMeToken = string.Empty
                });
            }
        }

        /// <summary>
        /// Authenticates a user using a remember-me token obtained from standard login.
        /// On success returns a new JWT and a refreshed remember-me token.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing the remember-me token.</param>
        /// <returns>
        /// 200 with JWT on success; 400/401 on invalid token or input; 500 on server error.
        /// </returns>
        [HttpPost("token-login")]
        public async Task<ActionResult<TokenLoginResponse>> TokenLogin(
            [FromBody] TokenLoginRequest? request
        )
        {
            if (request is null)
            {
                return BadRequest(new TokenLoginResponse
                {
                    ErrorMessage = _errEmptyRequest,
                    Jwt = string.Empty,
                    RememberMeToken = string.Empty
                });
            }

            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return BadRequest(new TokenLoginResponse
                {
                    ErrorMessage = "Pusty token.",
                    Jwt = string.Empty,
                    RememberMeToken = string.Empty
                });
            }

            try
            {
                (bool result, string error, string? rememberToken, long? userId) = await _userController.AuthWithTokenAsync(request.Token);
                if (!result)
                    return Unauthorized(new TokenLoginResponse
                    {
                        ErrorMessage = "Niepoprawny token.",
                        Jwt = string.Empty,
                        RememberMeToken = string.Empty
                    });

                if (userId is null)
                    throw new Exception("Empty userId");

                return Ok(new TokenLoginResponse
                {
                    ErrorMessage = string.Empty,
                    Jwt = JwtUtils.Generate(_jwtOptions, (long)userId),
                    RememberMeToken = rememberToken ?? string.Empty
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new TokenLoginResponse
                {
                    ErrorMessage = _errInternalServer,
                    Jwt = string.Empty,
                    RememberMeToken = string.Empty
                });
            }
        }

        /// <summary>
        /// Partially updates user profile fields (e.g. names, phone).
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT and at least one profile field to update.</param>
        /// <returns>
        /// 200 with per-field update flags; 400 if nothing to update; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("update-data")]
        public async Task<ActionResult<UpdateDataResponse>> UpdateData(
            [FromBody] UpdateDataRequest? request
        )
        {
            if (request is null)
            {
                return BadRequest(new UpdateDataResponse
                {
                    ErrorMessage = _errEmptyRequest,
                    UserFirstNameUpdated = false,
                    UserSecondNameUpdated = false,
                    UserLastNameUpdated = false,
                    UserPhoneUpdated = false
                });
            }

            if (string.IsNullOrWhiteSpace(request.Jwt))
            {
                return BadRequest(new UpdateDataResponse
                {
                    ErrorMessage = _errLoginRequired,
                    UserFirstNameUpdated = false,
                    UserSecondNameUpdated = false,
                    UserLastNameUpdated = false,
                    UserPhoneUpdated = false
                });
            }

            bool hasAnyUpdate =
                !string.IsNullOrWhiteSpace(request.UserFirstName) ||
                !string.IsNullOrWhiteSpace(request.UserSecondName) ||
                !string.IsNullOrWhiteSpace(request.UserLastName) ||
                !string.IsNullOrWhiteSpace(request.UserPhone);


            if (!hasAnyUpdate)
                return BadRequest(new UpdateDataResponse
                {
                    ErrorMessage = _errNothingToUpdate,
                    UserFirstNameUpdated = false,
                    UserSecondNameUpdated = false,
                    UserLastNameUpdated = false,
                    UserPhoneUpdated = false
                });

            if (!string.IsNullOrWhiteSpace(request.UserPhone))
            {
                if (!_phoneRegex.IsMatch(request.UserPhone))
                    return BadRequest(new UpdateDataResponse
                    { 
                        ErrorMessage = "Niepoprawny format numeru telefonu.",
                        UserFirstNameUpdated = false,
                        UserSecondNameUpdated = false,
                        UserLastNameUpdated = false,
                        UserPhoneUpdated = false
                    });
            }

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);                
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new UpdateDataResponse 
                { 
                    ErrorMessage = _errJwtExpired,
                    UserFirstNameUpdated = false,
                    UserSecondNameUpdated = false,
                    UserLastNameUpdated = false,
                    UserPhoneUpdated = false
                });
            }
            catch
            {
                return Unauthorized(new UpdateDataResponse
                {
                    ErrorMessage = _errJwtInvalid,
                    UserFirstNameUpdated = false,
                    UserSecondNameUpdated = false,
                    UserLastNameUpdated = false,
                    UserPhoneUpdated = false
                });
            }

            try
            {
                Dictionary<string, string> dataFields = new(4);

                if (!string.IsNullOrWhiteSpace(request.UserFirstName))
                    dataFields.Add(_fieldUserFirstName, request.UserFirstName);

                if (!string.IsNullOrWhiteSpace(request.UserSecondName))
                    dataFields.Add(_fieldUserSecondName, request.UserSecondName);

                if (!string.IsNullOrWhiteSpace(request.UserLastName))
                    dataFields.Add(_fieldUserLastName, request.UserLastName);

                if (!string.IsNullOrWhiteSpace(request.UserPhone))
                    dataFields.Add(_fieldUserPhone, request.UserPhone);

                Dictionary<string, bool> fieldResults = await _userController.UpdateDataAsync(userId, dataFields);

                return Ok(new UpdateDataResponse
                {
                    ErrorMessage = string.Empty,
                    UserFirstNameUpdated = fieldResults.TryGetValue(_fieldUserFirstName, out bool first) && first,
                    UserSecondNameUpdated = fieldResults.TryGetValue(_fieldUserSecondName, out bool second) && second,
                    UserLastNameUpdated = fieldResults.TryGetValue(_fieldUserLastName, out bool last) && last,
                    UserPhoneUpdated = fieldResults.TryGetValue(_fieldUserPhone, out bool phone) && phone
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new UpdateDataResponse
                { 
                    ErrorMessage = _errInternalServer,
                    UserFirstNameUpdated = false,
                    UserSecondNameUpdated = false,
                    UserLastNameUpdated = false,
                    UserPhoneUpdated = false
                });
            }
        }

        /// <summary>
        /// Partially updates ranking-algorithm weights used by the backend.
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT and at least one non-empty weights array to update.</param>
        /// <returns>
        /// 200 with per-field update flags; 400 if nothing to update; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("update-weights")]
        public async Task<ActionResult<UpdateWeightsResponse>> UpdateWeights(
            [FromBody] UpdateWeightsRequest? request
        )
        {
            if (request is null)
                return BadRequest(new UpdateWeightsResponse { ErrorMessage = _errEmptyRequest });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new UpdateWeightsResponse { ErrorMessage = _errLoginRequired });

            bool hasAnyUpdate =
                (request.OrderByOption is not null && request.OrderByOption.Length > 0) ||
                (request.MeanValueIds is not null && request.MeanValueIds.Length > 0) ||
                (request.Vector is not null && request.Vector.Length > 0) ||
                (request.MeanDist is not null && request.MeanDist.Length > 0) ||
                (request.MeansValueSum is not null && request.MeansValueSum.Length > 0) ||
                (request.MeansValueSsum is not null && request.MeansValueSsum.Length > 0) ||
                (request.MeansValueCount is not null && request.MeansValueCount.Length > 0) ||
                (request.MeansWeightSum is not null && request.MeansWeightSum.Length > 0) ||
                (request.MeansWeightSsum is not null && request.MeansWeightSsum.Length > 0) ||
                (request.MeansWeightCount is not null && request.MeansWeightCount.Length > 0);

            if (!hasAnyUpdate)
                return BadRequest(new UpdateWeightsResponse { ErrorMessage = _errNothingToUpdate });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new UpdateWeightsResponse { ErrorMessage = _errJwtExpired });
            }
            catch
            {
                return Unauthorized(new UpdateWeightsResponse { ErrorMessage = _errJwtInvalid });
            }

            try
            {
                Dictionary<string, object> dataFields = new(10);

                if (request.OrderByOption is not null && request.OrderByOption.Length > 0)
                    dataFields.Add(_fieldOrderByOption, request.OrderByOption);

                if (request.MeanValueIds is not null && request.MeanValueIds.Length > 0)
                    dataFields.Add(_fieldMeanValueIds, request.MeanValueIds);

                if (request.Vector is not null && request.Vector.Length > 0)
                    dataFields.Add(_fieldVector, request.Vector);

                if (request.MeanDist is not null && request.MeanDist.Length > 0)
                    dataFields.Add(_fieldMeanDist, request.MeanDist);

                if (request.MeansValueSum is not null && request.MeansValueSum.Length > 0)
                    dataFields.Add(_fieldMeansValueSum, request.MeansValueSum);

                if (request.MeansValueSsum is not null && request.MeansValueSsum.Length > 0)
                    dataFields.Add(_fieldMeansValueSsum, request.MeansValueSsum);

                if (request.MeansValueCount is not null && request.MeansValueCount.Length > 0)
                    dataFields.Add(_fieldMeansValueCount, request.MeansValueCount);

                if (request.MeansWeightSum is not null && request.MeansWeightSum.Length > 0)
                    dataFields.Add(_fieldMeansWeightSum, request.MeansWeightSum);

                if (request.MeansWeightSsum is not null && request.MeansWeightSsum.Length > 0)
                    dataFields.Add(_fieldMeansWeightSsum, request.MeansWeightSsum);

                if (request.MeansWeightCount is not null && request.MeansWeightCount.Length > 0)
                    dataFields.Add(_fieldMeansWeightCount, request.MeansWeightCount);

                Dictionary<string, bool> fieldResults = await _userController.UpdateWeightsAsync(userId, dataFields);

                return Ok(new UpdateWeightsResponse
                {
                    ErrorMessage = string.Empty,
                    OrderByOptionUpdated = fieldResults.TryGetValue(_fieldOrderByOption, out bool obo) && obo,
                    MeanValueIdsUpdated = fieldResults.TryGetValue(_fieldMeanValueIds, out bool mvi) && mvi,
                    VectorUpdated = fieldResults.TryGetValue(_fieldVector, out bool vec) && vec,
                    MeanDistUpdated = fieldResults.TryGetValue(_fieldMeanDist, out bool md) && md,
                    MeansValueSumUpdated = fieldResults.TryGetValue(_fieldMeansValueSum, out bool mvs) && mvs,
                    MeansValueSsumUpdated = fieldResults.TryGetValue(_fieldMeansValueSsum, out bool mvss) && mvss,
                    MeansValueCountUpdated = fieldResults.TryGetValue(_fieldMeansValueCount, out bool mvc) && mvc,
                    MeansWeightSumUpdated = fieldResults.TryGetValue(_fieldMeansWeightSum, out bool mws) && mws,
                    MeansWeightSsumUpdated = fieldResults.TryGetValue(_fieldMeansWeightSsum, out bool mwss) && mwss,
                    MeansWeightCountUpdated = fieldResults.TryGetValue(_fieldMeansWeightCount, out bool mwc) && mwc
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new UpdateWeightsResponse { ErrorMessage = _errInternalServer });
            }
        }

        /// <summary>
        /// Changes the user password.
        /// Requires a valid JWT provided in the request body; the new password must satisfy the password policy.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT and the new password.</param>
        /// <returns>
        /// 200 on success; 400 on invalid input; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("change-password")]
        public async Task<ActionResult<ChangePasswordResponse>> ChangePassword(
            [FromBody] ChangePasswordRequest? request
        )
        {
            if (request is null)
                return BadRequest(new ChangePasswordResponse { ErrorMessage = _errEmptyRequest });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new ChangePasswordResponse { ErrorMessage = _errLoginRequired });

            if (string.IsNullOrWhiteSpace(request.NewPassword))
                return BadRequest(new ChangePasswordResponse { ErrorMessage = "Puste hasło." });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new ChangePasswordResponse { ErrorMessage = _errJwtExpired });
            }
            catch
            {
                return Unauthorized(new ChangePasswordResponse { ErrorMessage = _errJwtInvalid });
            }

            try
            {
                bool result = await _userController.ChangePasswordAsync(userId, _userPasswordPolicy, request.NewPassword);
                if (!result)
                    throw new Exception("Password change failed");

                return Ok(new ChangePasswordResponse { ErrorMessage = string.Empty });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ChangePasswordResponse { ErrorMessage = _errInternalServer });
            }
        }

        /// <summary>
        /// Logs the user out (invalidates the current session on the backend side).
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT.</param>
        /// <returns>
        /// 200 on success; 400 on invalid input; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("logout")]
        public async Task<ActionResult<LogoutResponse>> Logout(
            [FromBody] LogoutRequest? request
        )
        {
            if (request is null)
                return BadRequest(new LogoutResponse { ErrorMessage = _errEmptyRequest });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new LogoutResponse { ErrorMessage = _errLoginRequired });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new LogoutResponse { ErrorMessage = _errJwtExpired });
            }
            catch
            {
                return Unauthorized(new LogoutResponse { ErrorMessage = _errJwtInvalid });
            }

            try
            {
                await _userController.LogoutAsync(userId);

                return Ok(new LogoutResponse { ErrorMessage = string.Empty });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new LogoutResponse { ErrorMessage = _errInternalServer });
            }
        }

        /// <summary>
        /// Checks whether the authenticated user has a given permission.
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT and permission name.</param>
        /// <returns>
        /// 200 if the user has the permission; 403 if not; 400 on invalid input; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("check-permission")]
        public async Task<ActionResult<CheckPermissionResponse>> CheckPermission(
            [FromBody] CheckPermissionRequest? request
        )
        {
            if (request is null)
                return BadRequest(new CheckPermissionResponse { ErrorMessage = _errEmptyRequest });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new CheckPermissionResponse { ErrorMessage = _errLoginRequired });

            if (string.IsNullOrWhiteSpace(request.PermissionName))
                return BadRequest(new CheckPermissionResponse { ErrorMessage = "Pusta nazwa permisji." });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new CheckPermissionResponse { ErrorMessage = _errJwtExpired });
            }
            catch
            {
                return Unauthorized(new CheckPermissionResponse { ErrorMessage = _errJwtInvalid });
            }

            try
            {
                bool hasPermission = await _userController.CheckPermissionAsync(userId, request.PermissionName);

                if (!hasPermission)
                    return StatusCode(StatusCodes.Status403Forbidden, new CheckPermissionResponse { ErrorMessage = "Brak uprawnień." });

                return Ok(new CheckPermissionResponse { ErrorMessage = string.Empty });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CheckPermissionResponse { ErrorMessage = _errInternalServer });
            }
        }

        /// <summary>
        /// Stores a snapshot of search parameters in the user's search history.
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT and at least one search field to store.</param>
        /// <returns>
        /// 200 on success; 400 if no search data provided; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("insert-search-history")]
        public async Task<ActionResult<InsertSearchHistoryResponse>> InsertSearchHistory(
            [FromBody] InsertSearchHistoryRequest? request
        )
        {
            if (request is null)
                return BadRequest(new InsertSearchHistoryResponse { ErrorMessage = _errEmptyRequest });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new InsertSearchHistoryResponse { ErrorMessage = _errLoginRequired });

            bool hasAnyUpdate =
                !string.IsNullOrWhiteSpace(request.Keywords) ||
                (request.Distance is not null) ||
                (request.IsRemote is not null) ||
                (request.IsHybrid is not null) ||
                (request.LeadingCategoryId is not null) ||
                (request.CityId is not null) ||
                (request.SalaryFrom is not null) ||
                (request.SalaryTo is not null) ||
                (request.SalaryPeriodId is not null) ||
                (request.SalaryCurrencyId is not null) ||
                (request.EmploymentScheduleIds is not null && request.EmploymentScheduleIds.Length > 0) ||
                (request.EmploymentTypeIds is not null && request.EmploymentTypeIds.Length > 0);

            if (!hasAnyUpdate)
                return BadRequest(new InsertSearchHistoryResponse { ErrorMessage = _errNothingToUpdate });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new InsertSearchHistoryResponse { ErrorMessage = _errJwtExpired });
            }
            catch
            {
                return Unauthorized(new InsertSearchHistoryResponse { ErrorMessage = _errJwtInvalid });
            }
            
            try
            {
                bool result = await _userController.InsertSearchHistoryAsync(
                    userId, request.Keywords, request.Distance,
                    request.IsRemote, request.IsHybrid, request.LeadingCategoryId,
                    request.CityId, request.SalaryFrom, request.SalaryTo,
                    request.SalaryPeriodId, request.SalaryCurrencyId,
                    request.EmploymentScheduleIds, request.EmploymentTypeIds
                );

                if (!result)
                    throw new Exception("Inserting history failed");

                return Ok(new InsertSearchHistoryResponse { ErrorMessage = string.Empty });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new InsertSearchHistoryResponse { ErrorMessage = _errInternalServer });
            }
        }

        /// <summary>
        /// Deletes the authenticated user's account.
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT.</param>
        /// <returns>
        /// 200 on success; 400 on invalid input; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("delete-user")]
        public async Task<ActionResult<DeleteUserResponse>> DeleteUser(
            [FromBody] DeleteUserRequest? request
        )
        {
            if (request is null)
                return BadRequest(new DeleteUserResponse { ErrorMessage = _errEmptyRequest });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new DeleteUserResponse { ErrorMessage = _errLoginRequired });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new DeleteUserResponse { ErrorMessage = _errJwtExpired });
            }
            catch
            {
                return Unauthorized(new DeleteUserResponse { ErrorMessage = _errJwtInvalid });
            }

            try
            {
                bool result = await _userController.DeleteUserAsync(userId);
                if (!result)
                    throw new Exception("Removing user failed");

                return Ok(new DeleteUserResponse { ErrorMessage = string.Empty });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new DeleteUserResponse { ErrorMessage = _errInternalServer });
            }
        }

        /// <summary>
        /// Issues a fresh JWT for an authenticated user.
        /// Requires a valid (not expired) JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT.</param>
        /// <returns>
        /// 200 with a new JWT on success; 400 on invalid input; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("refresh-jwt")]
        public async Task<ActionResult<RefreshJwtResponse>> RefreshJwt(
            [FromBody] RefreshJwtRequest? request
        )
        {
            if (request is null)
                return BadRequest(new RefreshJwtResponse { ErrorMessage = _errEmptyRequest, Jwt = string.Empty });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new RefreshJwtResponse { ErrorMessage = _errLoginRequired, Jwt = string.Empty });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new RefreshJwtResponse { ErrorMessage = _errJwtExpired, Jwt = string.Empty });
            }
            catch
            {
                return Unauthorized(new RefreshJwtResponse { ErrorMessage = _errJwtInvalid, Jwt = string.Empty });
            }

            try
            {
                return Ok(new RefreshJwtResponse
                {
                    ErrorMessage = string.Empty,
                    Jwt = JwtUtils.Generate(_jwtOptions, userId)
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RefreshJwtResponse 
                {
                    ErrorMessage = _errInternalServer,
                    Jwt = string.Empty
                });
            }
        }

        /// <summary>
        /// Validates whether the provided JWT is accepted by the backend.
        /// Intended as a lightweight session validity check.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT.</param>
        /// <returns>
        /// 200 with <c>result=true</c> when valid; 400/401 with <c>result=false</c> otherwise.
        /// </returns>
        [HttpPost("check-jwt")]
        public async Task<ActionResult<CheckJwtResponse>> RefreshJwt(
            [FromBody] CheckJwtRequest? request
        )
        {
            if (request is null)
                return BadRequest(new CheckJwtResponse());

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new CheckJwtResponse());

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new CheckJwtResponse());
            }
            catch
            {
                return Unauthorized(new CheckJwtResponse());
            }

            return Ok(new CheckJwtResponse { Result = true });
        }

        /// <summary>
        /// Returns the last N stored searches for the authenticated user.
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT and the limit (N).</param>
        /// <returns>
        /// 200 with search history payload; 400 on invalid input; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("get-search-history")]
        public async Task<ActionResult<GetSearchHistoryResponse>> GetSearchHistory(
            [FromBody] GetSearchHistoryRequest? request
        )
        {
            if (request is null)
                return BadRequest(new GetSearchHistoryResponse { ErrorMessage = _errEmptyRequest, SearchHistory = JsonDocument.Parse("[]").RootElement.Clone() });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new GetSearchHistoryResponse { ErrorMessage = _errLoginRequired, SearchHistory = JsonDocument.Parse("[]").RootElement.Clone() });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new GetSearchHistoryResponse { ErrorMessage = _errJwtExpired, SearchHistory = JsonDocument.Parse("[]").RootElement.Clone() });
            }
            catch
            {
                return Unauthorized(new GetSearchHistoryResponse { ErrorMessage = _errJwtInvalid, SearchHistory = JsonDocument.Parse("[]").RootElement.Clone() });
            }

            try
            {
                string json = await _userController.GetSearchHistoryAsync(userId, request.Limit, HttpContext.RequestAborted);
                using JsonDocument doc = JsonDocument.Parse(json);

                return Ok(new GetSearchHistoryResponse
                {
                    ErrorMessage = string.Empty,
                    SearchHistory = doc.RootElement.Clone()
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GetSearchHistoryResponse
                {
                    ErrorMessage = _errInternalServer,
                    SearchHistory = JsonDocument.Parse("[]").RootElement.Clone()
                });
            }
        }

        /// <summary>
        /// Fetches stored weights for the authenticated user.
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT.</param>
        /// <returns>
        /// 200 with weights payload; 400 on invalid input; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("get-weights")]
        public async Task<ActionResult<GetWeightsResponse>> GetWeights(
            [FromBody] GetWeightsRequest? request
        )
        {
            if (request is null)
                return BadRequest(new GetWeightsResponse { ErrorMessage = _errEmptyRequest, Weights = JsonDocument.Parse("{}").RootElement.Clone() });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new GetWeightsResponse { ErrorMessage = _errLoginRequired, Weights = JsonDocument.Parse("{}").RootElement.Clone() });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new GetWeightsResponse { ErrorMessage = _errJwtExpired, Weights = JsonDocument.Parse("{}").RootElement.Clone() });
            }
            catch
            {
                return Unauthorized(new GetWeightsResponse { ErrorMessage = _errJwtInvalid, Weights = JsonDocument.Parse("{}").RootElement.Clone() });
            }

            try
            {
                string json = await _userController.GetWeightsAsync(userId, HttpContext.RequestAborted);
                using JsonDocument doc = JsonDocument.Parse(json);

                return Ok(new GetWeightsResponse
                {
                    ErrorMessage = string.Empty,
                    Weights = doc.RootElement.Clone()
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GetWeightsResponse
                {
                    ErrorMessage = _errInternalServer,
                    Weights = JsonDocument.Parse("{}").RootElement.Clone()
                });
            }
        }

        /// <summary>
        /// Fetches user profile data for the authenticated user.
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT.</param>
        /// <returns>
        /// 200 with user data payload; 400 on invalid input; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("get-data")]
        public async Task<ActionResult<GetDataResponse>> GetData(
            [FromBody] GetDataRequest? request
        )
        {
            if (request is null)
                return BadRequest(new GetDataResponse { ErrorMessage = _errEmptyRequest, UserData = JsonDocument.Parse("{}").RootElement.Clone() });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new GetDataResponse { ErrorMessage = _errLoginRequired, UserData = JsonDocument.Parse("{}").RootElement.Clone() });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new GetDataResponse { ErrorMessage = _errJwtExpired, UserData = JsonDocument.Parse("{}").RootElement.Clone() });
            }
            catch
            {
                return Unauthorized(new GetDataResponse { ErrorMessage = _errJwtInvalid, UserData = JsonDocument.Parse("{}").RootElement.Clone() });
            }

            try
            {
                string json = await _userController.GetDataAsync(userId, HttpContext.RequestAborted);
                using JsonDocument doc = JsonDocument.Parse(json);

                return Ok(new GetDataResponse
                {
                    ErrorMessage = string.Empty,
                    UserData = doc.RootElement.Clone()
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GetDataResponse
                {
                    ErrorMessage = _errInternalServer,
                    UserData = JsonDocument.Parse("{}").RootElement.Clone()
                });
            }
        }

        /// <summary>
        /// Partially updates personal offer preferences for the authenticated user.
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples (including required paired fields), see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT and any subset of preference fields to update.</param>
        /// <returns>
        /// 200 with per-field update flags; 400 if nothing to update; 401 if JWT is invalid/expired; 500 on server error.
        /// </returns>
        [HttpPost("update-preferences")]
        public async Task<ActionResult<UpdatePreferencesResponse>> UpdatePreferences(
            [FromBody] UpdatePreferencesRequest? request
        )
        {
            if (request is null)
                return BadRequest(new UpdatePreferencesResponse { ErrorMessage = _errEmptyRequest });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new UpdatePreferencesResponse { ErrorMessage = _errLoginRequired });

            bool hasAnyUpdate =
                request.LeadingCategoryId is not null ||
                request.SalaryFrom is not null ||
                request.SalaryTo is not null ||
                (request.EmploymentTypeIds is not null && request.EmploymentTypeIds.Length > 0) ||
                (request.LanguageId is not null && request.LanguageLevelId is not null) ||
                !string.IsNullOrWhiteSpace(request.JobStatusName) ||
                !string.IsNullOrWhiteSpace(request.CityName) ||
                (request.WorkTypeNames is not null && request.WorkTypeNames.Length > 0) ||
                (
                    request.SkillNames is not null && request.SkillNames.Length > 0 &&
                    request.SkillMonths is not null && request.SkillMonths.Length > 0
                );

            if (!hasAnyUpdate)
                return BadRequest(new UpdatePreferencesResponse { ErrorMessage = _errNothingToUpdate });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new UpdatePreferencesResponse { ErrorMessage = _errJwtExpired });
            }
            catch
            {
                return Unauthorized(new UpdatePreferencesResponse { ErrorMessage = _errJwtInvalid });
            }

            Dictionary<string, object> dataFields = new(8);

            if (request.LeadingCategoryId is not null)
                dataFields.Add(_fieldLeadingCategory, request.LeadingCategoryId.Value);

            if (request.SalaryFrom is not null || request.SalaryTo is not null)
                dataFields.Add(_fieldSalaryRange, Tuple.Create(request.SalaryFrom, request.SalaryTo));

            if (request.EmploymentTypeIds is not null && request.EmploymentTypeIds.Length > 0)
                dataFields.Add(_fieldEmploymentTypes, request.EmploymentTypeIds);

            if (request.LanguageId is not null || request.LanguageLevelId is not null)
                dataFields.Add(_fieldLanguagePair, Tuple.Create(request.LanguageId, request.LanguageLevelId));

            if (!string.IsNullOrWhiteSpace(request.JobStatusName))
                dataFields.Add(_fieldJobStatus, request.JobStatusName);

            if (!string.IsNullOrWhiteSpace(request.CityName))
                dataFields.Add(_fieldCity, request.CityName);

            if (request.WorkTypeNames is not null && request.WorkTypeNames.Length > 0)
                dataFields.Add(_fieldWorkTypes, request.WorkTypeNames);

            if (request.SkillNames is not null && request.SkillMonths is not null &&
                request.SkillNames.Length == request.SkillMonths.Length)
            {
                dataFields.Add(_fieldSkills, Tuple.Create(request.SkillNames, request.SkillMonths));
            }

            try
            {
                Dictionary<string, bool> fieldResults = await _userController.UpdatePreferencesAsync(userId, dataFields);

                return Ok(new UpdatePreferencesResponse
                {
                    ErrorMessage = string.Empty,

                    LeadingCategoryIdUpdated =
                    fieldResults.TryGetValue(_fieldLeadingCategory, out bool lc) && lc,

                    SalaryFromUpdated =
                    fieldResults.TryGetValue(_fieldSalaryRange, out bool srFrom) && srFrom,
                    SalaryToUpdated =
                    fieldResults.TryGetValue(_fieldSalaryRange, out bool srTo) && srTo,

                    EmploymentTypeIdsUpdated =
                    fieldResults.TryGetValue(_fieldEmploymentTypes, out bool et) && et,

                    LanguageIdUpdated =
                    fieldResults.TryGetValue(_fieldLanguagePair, out bool lpLang) && lpLang,
                    LanguageLevelIdUpdated =
                    fieldResults.TryGetValue(_fieldLanguagePair, out bool lpLvl) && lpLvl,

                    JobStatusNameUpdated =
                    fieldResults.TryGetValue(_fieldJobStatus, out bool js) && js,

                    CityNameUpdated =
                    fieldResults.TryGetValue(_fieldCity, out bool city) && city,

                    WorkTypeNamesUpdated =
                    fieldResults.TryGetValue(_fieldWorkTypes, out bool wt) && wt,

                    SkillNamesUpdated =
                    fieldResults.TryGetValue(_fieldSkills, out bool skNames) && skNames,
                    SkillMonthsUpdated =
                    fieldResults.TryGetValue(_fieldSkills, out bool skMonths) && skMonths
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new UpdatePreferencesResponse { ErrorMessage = _errInternalServer });
            }
        }

        /// <summary>
        /// Returns saved preferences formatted as JSON.
        /// If preferences are incomplete, the endpoint returns 422 with a partial payload.
        /// Requires a valid JWT provided in the request body.
        /// For full request/response details and examples, see <c>UserAPI-DOCS.md</c>.
        /// </summary>
        /// <param name="request">Request body containing JWT.</param>
        /// <returns>
        /// 200 with complete preferences; 422 with partial preferences when incomplete; 400/401/500 on errors.
        /// </returns>
        [HttpPost("get-preferences")]
        public async Task<ActionResult<GetPreferencesResponse>> GetPreferences(
            [FromBody] GetPreferencesRequest? request
        )
        {
            if (request is null)
                return BadRequest(new GetPreferencesResponse { ErrorMessage = _errEmptyRequest, Preferences = JsonDocument.Parse("{}").RootElement.Clone() });

            if (string.IsNullOrWhiteSpace(request.Jwt))
                return BadRequest(new GetPreferencesResponse { ErrorMessage = _errLoginRequired, Preferences = JsonDocument.Parse("{}").RootElement.Clone() });

            long userId;
            try
            {
                userId = JwtUtils.GetUserId(_jwtOptions, request.Jwt);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new GetPreferencesResponse { ErrorMessage = _errJwtExpired, Preferences = JsonDocument.Parse("{}").RootElement.Clone() });
            }
            catch
            {
                return Unauthorized(new GetPreferencesResponse { ErrorMessage = _errJwtInvalid, Preferences = JsonDocument.Parse("{}").RootElement.Clone() });
            }

            try
            {
                (bool result, UserPreferencesResult? userPreferences) = await _userController.GetUserPreferencesAsync(userId);
                if (!result || userPreferences is null)
                    throw new Exception("Retrieving preferences failed");

                // Validate paired arrays (languages + skills)
                if (userPreferences.Languages.Length != userPreferences.LanguageLevels.Length)
                    throw new Exception("Languages and LanguageLevels must have the same length.");

                if (userPreferences.SkillNames.Length != userPreferences.SkillExperienceMonths.Length ||
                    userPreferences.SkillNames.Length != userPreferences.SkillEntryDates.Length)
                    throw new Exception("SkillNames, SkillExperienceMonths and SkillEntryDates must have the same length.");

                // Build payload in the desired JSON shape (with aggregated languages + skills)
                var payload = new
                {
                    leading_category_id = userPreferences.LeadingCategoryId,
                    salary_from = userPreferences.SalaryFrom,
                    salary_to = userPreferences.SalaryTo,

                    employment_type_ids = userPreferences.EmploymentTypes,

                    job_status = userPreferences.JobStatus,

                    city_name = userPreferences.CityName,
                    work_types = userPreferences.WorkTypes,

                    languages = userPreferences.Languages
                        .Select((languageId, index) => new
                        {
                            language_id = languageId,
                            language_level_id = userPreferences.LanguageLevels[index]
                        })
                        .ToArray(),

                    skills = userPreferences.SkillNames
                        .Select((skillName, index) => new
                        {
                            skill_name = skillName,
                            experience_months = userPreferences.SkillExperienceMonths[index],
                            entry_date = userPreferences.SkillEntryDates[index]
                        })
                        .ToArray()
                };

                byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(
                    payload,
                    new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    }
                );

                using JsonDocument doc = JsonDocument.Parse(jsonBytes);

                bool isIncomplete =
                    userPreferences.LeadingCategoryId is null || userPreferences.LeadingCategoryId == 0 ||
                    userPreferences.SalaryFrom is null || userPreferences.SalaryFrom == 0 ||
                    userPreferences.SalaryTo is null || userPreferences.SalaryTo == 0 ||

                    userPreferences.EmploymentTypes is null || userPreferences.EmploymentTypes.Length == 0 ||
                    userPreferences.EmploymentTypes.Any(x => x == 0) ||

                    string.IsNullOrWhiteSpace(userPreferences.JobStatus) ||

                    string.IsNullOrWhiteSpace(userPreferences.CityName) ||

                    userPreferences.WorkTypes is null || userPreferences.WorkTypes.Length == 0 ||
                    userPreferences.WorkTypes.Any(string.IsNullOrWhiteSpace) ||

                    userPreferences.Languages is null || userPreferences.Languages.Length == 0 ||
                    userPreferences.LanguageLevels is null || userPreferences.LanguageLevels.Length == 0 ||
                    userPreferences.Languages.Any(x => x == 0) ||
                    userPreferences.LanguageLevels.Any(x => x == 0) ||

                    userPreferences.SkillNames is null || userPreferences.SkillNames.Length == 0 ||
                    userPreferences.SkillExperienceMonths is null || userPreferences.SkillExperienceMonths.Length == 0 ||
                    userPreferences.SkillEntryDates is null || userPreferences.SkillEntryDates.Length == 0 ||

                    userPreferences.SkillNames.Any(string.IsNullOrWhiteSpace) ||
                    userPreferences.SkillExperienceMonths.Any(x => x == 0);

                if (isIncomplete)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new GetPreferencesResponse
                    {
                        ErrorMessage = "Preferences are incomplete",
                        Preferences = doc.RootElement.Clone()
                    });
                }

                return Ok(new GetPreferencesResponse
                {
                    ErrorMessage = string.Empty,
                    Preferences = doc.RootElement.Clone()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GetPreferencesResponse
                {
                    ErrorMessage = _errInternalServer,
                    Preferences = JsonDocument.Parse("{}").RootElement.Clone()
                });
            }
        }
    }
}
