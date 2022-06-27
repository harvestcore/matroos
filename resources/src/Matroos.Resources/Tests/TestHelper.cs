using Matroos.Resources.Classes.API;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace Matroos.Resources.Tests;

public static class TestHelper
{
    /// <summary>
    /// Get an status code based on the object type.
    /// </summary>
    /// <param name="resultType">The <see cref="ObjectResult"/> object.</param>
    /// <returns>The status code.</returns>
    public static int GetStatusCode(ObjectResult resultType)
    {
        if (resultType is OkObjectResult)
        {
            return 200;
        }
        else if (resultType is BadRequestObjectResult)
        {
            return 400;
        }
        else if (resultType is NotFoundObjectResult)
        {
            return 404;
        }

        return 0;
    }

    /// <summary>
    /// Checks the <see cref="SuccessResponse"/> status result object.
    /// </summary>
    /// <param name="res">The <see cref="ActionResult"/>.</param>
    /// <param name="status">Whether the response should be true or false.</param>
    public static void SuccessResponseShouldBe(this ActionResult<SuccessResponse> res, bool status)
    {
        Assert.NotNull(res);
        OkObjectResult? response = res.Result as OkObjectResult;

        Assert.NotNull(response);
        Assert.Equal(200, response?.StatusCode ?? 0);

        SuccessResponse? successResponse = response?.Value as SuccessResponse;
        Assert.Equal(status, successResponse?.Success ?? !status);
    }

    /// <summary>
    /// Check a <see cref="ActionResult"/> for a specific response type.
    /// </summary>
    /// <typeparam name="TValue">The response type.</typeparam>
    /// <param name="res">The <see cref="ActionResult<SuccessResponse>"/>.</param>
    public static void CheckResponse<TValue>(this ActionResult<SuccessResponse> res) where TValue : ObjectResult
    {
        Assert.NotNull(res);
        if (res == null || res.Result == null)
        {
            throw new ArgumentNullException(nameof(res));
        }

        TValue response = (TValue)res.Result;

        Assert.NotNull(response);
        Assert.Equal(GetStatusCode(response), response?.StatusCode ?? 0);
    }
}