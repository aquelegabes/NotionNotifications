using NotionNotifications.Integration.Enums;

namespace NotionNotifications.Integration.Models
{
    public class NotionNotificationResultFilterModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public bool AlreadyNotified { get; set; }
        public DateTimeOffset? EventDate { get; set; }
        public string[]? Categories { get; set; }
        public ESortDirection SortBy { get; set; } = ESortDirection.Descending;

        private void SetupTitleFilter(List<object> andFilter)
        {
            var titleFilter = Title is not null ?
                new
                {
                    property = "Título",
                    rich_text = new { contains = Title }
                }
                : null;

            if (titleFilter is not null)
                andFilter.Add(titleFilter);
        }

        private void SetupIdFilter(List<object> andFilter)
        {
            var idFilter = Id is not null ?
                new
                {
                    property = "ID",
                    value = Id
                }
                : null;

            if (idFilter is not null)
                andFilter.Add(idFilter);
        }

        private void SetupAlreadyNotifiedFilter(List<object> andFilter)
        {
            var alreadyNotifiedFilter = new
            {
                property = "Já Notificado?",
                checkbox = new { equals = AlreadyNotified }
            };

            andFilter.Add(alreadyNotifiedFilter);
        }

        private void SetupEventDateFilter(List<object> andFilter)
        {
            var eventDateFilter = EventDate is not null ?
                new
                {
                    property = "Data do evento",
                    date = new { equals = EventDate.GetValueOrDefault().ToString("yyyy-MM-dd") }
                } : null;

            if (eventDateFilter is not null)
                andFilter.Add(eventDateFilter);
        }

        private void SetupCategoriesFilter(List<object> andFilter)
        {
            var categoriesFilter = Categories?.Length > 0 ?
                Categories.Select(category => new
                {
                    property = "Categorias",
                    multi_select = new { contains = category }
                }) : null;

            if (categoriesFilter is not null)
                andFilter.AddRange(categoriesFilter);
        }

        private void SetupAndFilters(List<object> andFilter)
        {
            SetupTitleFilter(andFilter);
            SetupIdFilter(andFilter);
            SetupAlreadyNotifiedFilter(andFilter);
            SetupEventDateFilter(andFilter);
            SetupCategoriesFilter(andFilter);
        }

        internal object ToIntegrationFilter(
            int pageSize,
            string? startCursor = null)
        {
            List<object> andFilter = [
                new {
                    property = "É notificação?",
                    checkbox = new { equals = true }
                }];

            SetupAndFilters(andFilter);

            var filter = new Dictionary<string, object>
            {
                { "and", andFilter },
                { "sorts", new[] {
                        new { property = "Data do evento", direction = SortBy.ToString().ToLower() }
                    }.ToArray()
                },
                { "page_size", pageSize }
            };

            if (startCursor != null)
                filter.Add("start_cursor", startCursor);

            return filter;
        }
    }
}
