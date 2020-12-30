using System;
using System.Windows.Forms;
using vcs_nats;

namespace FormPractice
{
    public class SubClient
    {
        private static NatsClient natsClient;

        public static bool NatsStart(string url, string subject)
        {
            natsClient = new NatsClient(url, 10, 100);
            if (!natsClient.Connction())
            {
                return false;
            }
            Console.WriteLine("Connected");

            Action<byte[], string> action = (x, y) =>
            {
                Array.Reverse(x);
                int size = x.Length / sizeof(double);
                double[] doubleData = new double[size];

                //string str = System.Text.Encoding.Default.GetString(x);
                //Console.WriteLine(str);

                Buffer.BlockCopy(x, 0, doubleData, 0, size * sizeof(double));

                Console.WriteLine($"message total size : {size} / message : {string.Join(", ", doubleData)}");
            };

            natsClient.StartSubscription(subject, action);
            return true;
        }


        public static void NatsStop()
        {
            try
            {
                if (natsClient != null)
                {
                    natsClient.StopSubscription();
                    Console.WriteLine("stop subscription");
                    natsClient.Disconnect();
                    Console.WriteLine("disconnect");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
