using form = System.Windows.Forms;

namespace Relógio_Analógico
{
    public partial class Form1 : Form
    {
        form.Timer contador = new();

        int largura = 300, altura = 300, segundosHandler = 140, minutosHandler = 110, horasHandler = 80;
        int coordenadaX, coordenadaY;

        Bitmap bitmap;
        Graphics graphics;

        private void Form1_Load_1(object sender, EventArgs e)
        {
            bitmap = new Bitmap(largura + 1, altura + 1);

            coordenadaX = largura / 2;
            coordenadaY = altura / 2;

            this.BackColor = Color.White;

            contador.Interval = 1000;
            contador.Tick += new EventHandler(this.contador_Tick);
            contador.Start();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void contador_Tick(object sender, EventArgs e)
        {
            graphics = Graphics.FromImage(bitmap);

            int segundos = DateTime.Now.Second;
            int minutos = DateTime.Now.Minute;
            int horas = DateTime.Now.Hour;

            int[] handlerCoordenadas = new int[2];

            graphics.Clear(Color.White);

            graphics.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, largura, altura);

            graphics.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(140, 2));
            graphics.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(286, 140));
            graphics.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
            graphics.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 140));

            handlerCoordenadas = msCoord(segundos, segundosHandler);
            graphics.DrawLine(new Pen(Color.Red, 1f), new Point(coordenadaX, coordenadaY), new Point(handlerCoordenadas[0], handlerCoordenadas[1]));

            handlerCoordenadas = msCoord(minutos, minutosHandler);
            graphics.DrawLine(new Pen(Color.Black, 2f), new Point(coordenadaX, coordenadaY), new Point(handlerCoordenadas[0], handlerCoordenadas[1]));

            handlerCoordenadas = hrCoord(horas % 12, minutos, horasHandler);
            graphics.DrawLine(new Pen(Color.Gray, 3f), new Point(coordenadaX, coordenadaY), new Point(handlerCoordenadas[0], handlerCoordenadas[1]));

            pictureBox1.Image = bitmap;

            this.Text = "Relógio Analógico - " + horas + ":" + minutos + ":" + segundos;

            graphics.Dispose();
        }

        private int[] msCoord(int val, int hlen)
        {
            int[] coordenadas = new int[2];
            val *= 6;

            if (val >= 0 && val <= 180)
            {
                coordenadas[0] = coordenadaX + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coordenadas[1] = coordenadaY - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coordenadas[0] = coordenadaX - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coordenadas[1] = coordenadaY - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }

            return coordenadas;
        }

        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coordenadas = new int[2];

            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coordenadas[0] = coordenadaX + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coordenadas[1] = coordenadaY - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coordenadas[0] = coordenadaX - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coordenadas[1] = coordenadaY - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }

            return coordenadas;
        }
    }
}