// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Exceptions;
using HeyItIsMe.Core.Resources;

namespace HeyItIsMe.Core.Domain.Questions;

internal static class QuestionDomainExceptions
{
    public static DomainException ContentIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.Question_Content, typeof(SharedResource)) }
        );
    }

    public static DomainException DescriptionIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(SharedResource.Question_Description, typeof(SharedResource)) }
        );
    }
}
