namespace API.Utilities.Handlers
{
    public class GenerateHandler
    {
        public static string Nik(string? nik = null) 
        { 
            if (nik is null)
            {
                return "11111";
            }

            var generatedNik = int.Parse(nik) + 1;

            return generatedNik.ToString();
        }
    }
}
