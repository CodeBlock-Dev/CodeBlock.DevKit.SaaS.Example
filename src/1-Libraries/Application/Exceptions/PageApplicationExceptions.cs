// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CodeBlock.DevKit.AIChatBot.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using HeyItIsMe.Core.Resources;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace HeyItIsMe.Application.Exceptions;

internal static class PageApplicationExceptions
{
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

    public static ApplicationException FactGeneratorBotNotFound(string searchedKey)
    {
        return new ApplicationException(
            nameof(CoreResource.Record_Not_Found),
            typeof(CoreResource),
            new List<MessagePlaceholder>
            {
                MessagePlaceholder.CreateResource(AIChatBotResource.Bot, typeof(AIChatBotResource)),
                MessagePlaceholder.CreatePlainText(searchedKey),
            }
        );
    }

    public static ApplicationException RequiredPromptIsMissing()
    {
        return new ApplicationException(nameof(SharedResource.Required_Prompt_Is_Missing), typeof(SharedResource));
    }

    public static ApplicationException AIResponseFailed()
    {
        return new ApplicationException(nameof(SharedResource.AI_Response_Failed), typeof(SharedResource));
    }
}
