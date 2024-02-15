using Serilog.Events;

namespace JobPortalAPI.API.Configurations
{
    public class UsernameColumnWriter : IFormatProvider
    {
        public object? GetFormat(Type? formatType)
        {
            throw new NotImplementedException();
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            var (username, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
            throw new NotImplementedException();
        }
    }
}
