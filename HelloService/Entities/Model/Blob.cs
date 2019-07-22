using System;
namespace HelloService.Entities.Model
{
    public class Blob
    {
        public string Container { get; set; }
        public string Reff { get; set; }

        public Blob(string container, string reff)
        {
            Container = container;
            Reff = reff;
        }
    }
}
