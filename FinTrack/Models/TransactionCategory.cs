namespace FinTrack.Models
{
    public enum TransactionCategory
    {
        Trabalho, Alimentacao, Transporte,
        Moradia, Saude, Lazer, Educacao, Viagem, Outros
    }

    public static class TransactionCategoryExtensions
    {
        public static string ToEmoji(this TransactionCategory c) => c switch
        {
            TransactionCategory.Trabalho => "💰",
            TransactionCategory.Alimentacao => "🍔",
            TransactionCategory.Transporte => "🚗",
            TransactionCategory.Moradia => "🏠",
            TransactionCategory.Saude => "💊",
            TransactionCategory.Lazer => "🎮",
            TransactionCategory.Educacao => "📚",
            TransactionCategory.Viagem => "✈️",
            _ => "📦",
        };

        public static string ToLabel(this TransactionCategory c) => c switch
        {
            TransactionCategory.Trabalho => "Trabalho",
            TransactionCategory.Alimentacao => "Alimentação",
            TransactionCategory.Transporte => "Transporte",
            TransactionCategory.Moradia => "Moradia",
            TransactionCategory.Saude => "Saúde",
            TransactionCategory.Lazer => "Lazer",
            TransactionCategory.Educacao => "Educação",
            TransactionCategory.Viagem => "Viagem",
            _ => "Outros",
        };

        public static Color ToBadgeColor(this TransactionCategory c) => c switch
        {
            TransactionCategory.Trabalho => Color.FromArgb("#1E00E5A0"),
            TransactionCategory.Alimentacao => Color.FromArgb("#1EFF5C6A"),
            TransactionCategory.Transporte => Color.FromArgb("#1EFF5C6A"),
            TransactionCategory.Moradia => Color.FromArgb("#1EF0C040"),
            TransactionCategory.Saude => Color.FromArgb("#1EFF5C6A"),
            TransactionCategory.Lazer => Color.FromArgb("#1E00E5A0"),
            TransactionCategory.Educacao => Color.FromArgb("#1E00E5A0"),
            TransactionCategory.Viagem => Color.FromArgb("#1EF0C040"),
            _ => Color.FromArgb("#1E5A6070"),
        };

        public static Color ToBadgeTextColor(this TransactionCategory c) => c switch
        {
            TransactionCategory.Trabalho => Color.FromArgb("#00E5A0"),
            TransactionCategory.Alimentacao => Color.FromArgb("#FF5C6A"),
            TransactionCategory.Transporte => Color.FromArgb("#FF5C6A"),
            TransactionCategory.Moradia => Color.FromArgb("#F0C040"),
            TransactionCategory.Saude => Color.FromArgb("#FF5C6A"),
            TransactionCategory.Lazer => Color.FromArgb("#00E5A0"),
            TransactionCategory.Educacao => Color.FromArgb("#00E5A0"),
            TransactionCategory.Viagem => Color.FromArgb("#F0C040"),
            _ => Color.FromArgb("#5A6070"),
        };
    }
}