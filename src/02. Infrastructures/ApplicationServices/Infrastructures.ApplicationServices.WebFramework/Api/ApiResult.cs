using Framework.ApplicationServices.Common;
using Framework.Tools.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructures.ApplicationServices.WebFramework.Api
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToDisplay();
        }

        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        /// <summary>
        /// ناقص است
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResult(StatusCodeResult result)
        {
            if (result.StatusCode.ToString().StartsWith("5"))
                return new ApiResult(false, ApiResultStatusCode.ServerError, null);
            else if (result.StatusCode == 409)
                return new ApiResult(false, ApiResultStatusCode.LogicError, null);
            else
                return new ApiResult(false, ApiResultStatusCode.Empty);
        }

        public static implicit operator ApiResult(UnauthorizedResult data)
        {
            return new ApiResult(false, ApiResultStatusCode.UnAuthorized, null);
        }

        public static implicit operator ApiResult(EmptyResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.Empty, null);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResult(false, ApiResultStatusCode.BadRequest, message);
        }

        public static implicit operator ApiResult(NoContentResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.NoContent, null);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success, result.Content);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.NotFound);
        }
        #endregion
    }

    public class ApiResult<TData> : ApiResult
        where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null)
            : base(isSuccess, statusCode, message)
        {
            Data = data;
        }

        #region Implicit Operators
        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, data);
        }

        /// <summary>
        /// ناقص است
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator ApiResult<TData>(StatusCodeResult result)
        {
            if (result.StatusCode.ToString().StartsWith("5"))
                return new ApiResult<TData>(false, ApiResultStatusCode.ServerError, null);
            else if (result.StatusCode == 409)
                return new ApiResult<TData>(false, ApiResultStatusCode.LogicError, null);
            else
                return new ApiResult<TData>(false, ApiResultStatusCode.Empty, null);
        }

        public static implicit operator ApiResult<TData>(UnauthorizedResult data)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.UnAuthorized, null);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, (TData)result.Value);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null);
        }

        public static implicit operator ApiResult<TData>(EmptyResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.Empty, null);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null, message);
        }

        public static implicit operator ApiResult<TData>(ContentResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null, result.Content);
        }

        public static implicit operator ApiResult<TData>(NoContentResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.NoContent, null);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, null);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, (TData)result.Value);
        }
        #endregion
    }
}
