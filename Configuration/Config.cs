using System.Globalization;
namespace Configuration
{
    public class Config
    {
        //реализовать локализацию

        private CultureInfo cultureInto;

        //
        public Config(CultureInfo cultureInto)
        {
            this.cultureInto = CultureInfo.CurrentCulture;
        }
    }
}