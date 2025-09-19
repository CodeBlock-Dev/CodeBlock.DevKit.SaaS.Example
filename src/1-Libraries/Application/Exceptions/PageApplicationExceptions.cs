// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using HeyItIsMe.Core.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace HeyItIsMe.Application.Exceptions;

/// <summary>
/// Static factory class for creating application-specific exceptions related to Page operations.
/// This class demonstrates how to create custom exceptions with localized error messages and
/// proper error handling patterns.
/// </summary>
internal static class PageApplicationExceptions
{
    /// <summary>
    /// Creates an application exception when a page is not found by the specified search key.
    /// This method demonstrates how to create localized exceptions with placeholder values.
    /// </summary>
    /// <param name="searchedKey">The key that was used to search for the page (e.g., ID, route)</param>
    /// <returns>An application exception with localized error message</returns>
    /// <example>
    /// <code>
    /// if (page == null)
    ///     throw PageApplicationExceptions.PageNotFound(id);
    /// </code>
    /// </example>
    public static ApplicationException PageNotFound(string searchedKey)
    {
        return new ApplicationException(
            nameof(CoreResource.Record_Not_Found),
            typeof(CoreResource),
            new List<MessagePlaceholder>
            {
                MessagePlaceholder.CreateResource(SharedResource.Page, typeof(SharedResource)),
                MessagePlaceholder.CreatePlainText(searchedKey),
            }
        );
    }
}
