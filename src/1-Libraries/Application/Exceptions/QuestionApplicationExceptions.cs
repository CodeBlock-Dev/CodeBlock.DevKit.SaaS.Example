using HeyItIsMe.Core.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using ApplicationException = CodeBlock.DevKit.Application.Exceptions.ApplicationException;

namespace HeyItIsMe.Application.Exceptions;

internal static class QuestionApplicationExceptions
{
    public static ApplicationException QuestionNotFound(string searchedKey)
    {
        return new ApplicationException(
            nameof(CoreResource.Record_Not_Found),
            typeof(CoreResource),
            new List<MessagePlaceholder>
            {
                MessagePlaceholder.CreateResource(SharedResource.Question, typeof(SharedResource)),
                MessagePlaceholder.CreatePlainText(searchedKey),
            }
        );
    }
}
