using System;

namespace ProductSales.Application.Validations
{
    public static class IdentityValidator
    {
        public static bool VerifyIdentity(string identity)
        {
            string tcKimlikNo = identity;
            bool returnvalue = false;
            if (tcKimlikNo.Length == 11)
            {
                Calculate(tcKimlikNo, out long BTCNO, out long TcNo, out long Q1, out long Q2);

                returnvalue = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
            }


            if (returnvalue)
                return true;
            else
                return false;
        }

        private static void Calculate(string tcKimlikNo, out long BTCNO, out long TcNo, out long Q1, out long Q2)
        {
            Int64 ATCNO;
            long C1, C2, C3, C4, C5, C6, C7, C8, C9;

            TcNo = Int64.Parse(tcKimlikNo);

            ATCNO = TcNo / 100;
            BTCNO = TcNo / 100;

            C1 = ATCNO % 10; ATCNO /= 10;
            C2 = ATCNO % 10; ATCNO /= 10;
            C3 = ATCNO % 10; ATCNO /= 10;
            C4 = ATCNO % 10; ATCNO /= 10;
            C5 = ATCNO % 10; ATCNO /= 10;
            C6 = ATCNO % 10; ATCNO /= 10;
            C7 = ATCNO % 10; ATCNO /= 10;
            C8 = ATCNO % 10; ATCNO /= 10;
            C9 = ATCNO % 10;
            Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
            Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);
        }
    }
}
