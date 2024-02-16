using Microsoft.CodeAnalysis;

namespace Alchemy.SourceGenerator
{
    public static class DiagnosticDescriptors
    {
        const string Category = "GenerateAlchemySerializationData";

        public static readonly DiagnosticDescriptor MustBePartial = new(
            id: "ALCHEMY001",
            title: "AlchemySerialize class must be partial.",
            messageFormat: "AlchemySerialize class '{0}' must be partial",
            category: Category,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
    }
}