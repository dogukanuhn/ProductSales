namespace ProductSales.Application.Helpers
{
    public class JwtClaims
    {
        public JwtClaims(string claim)
        {
            Claim = claim;
        }
        public string Claim { get; set; }

        public override string ToString() => Claim;

        public static JwtClaims UserCode => new JwtClaims(nameof(UserCode));

    }
}