// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using HeyItIsMe.Core.Resources;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace HeyItIsMe.Application.Exceptions;

public static class ApplicationExceptions
{
    public static ApplicationException AIResponseFailed()
    {
        return new ApplicationException(nameof(SharedResource.AI_Response_Failed), typeof(SharedResource));
    }

    public static ApplicationException AIImageGenerationFailed()
    {
        return new ApplicationException(nameof(SharedResource.AI_Image_Generation_Failed), typeof(SharedResource));
    }
}
