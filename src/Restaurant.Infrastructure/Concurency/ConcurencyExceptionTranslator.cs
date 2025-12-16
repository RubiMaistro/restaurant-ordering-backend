using Microsoft.EntityFrameworkCore;

namespace Restaurant.Infrastructure.Concurency
{
    public static class ConcurencyExceptionTranslator
    {
        public static Exception Translate(DbUpdateConcurrencyException ex)
        {
            return new InvalidOperationException(
                "The resource was modified by another process.", ex);
        }
    }
}
