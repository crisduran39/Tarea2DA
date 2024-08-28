public class Mensaje
{
    public string Contenido { get; private set; }

    public Mensaje(string contenido)
    {
        Contenido = contenido;
    }

    public virtual string ObtenerContenido()
    {
        return Contenido;
    }
}
public abstract class MensajeDecorador : Mensaje
{
    protected Mensaje _mensaje;

    public MensajeDecorador(Mensaje mensaje) : base(mensaje.ObtenerContenido())
    {
        _mensaje = mensaje;
    }

    public override string ObtenerContenido()
    {
        return _mensaje.ObtenerContenido();
    }
}
public class EncriptarMensaje : MensajeDecorador
{
    public EncriptarMensaje(Mensaje mensaje) : base(mensaje) { }

    public override string ObtenerContenido()
    {
        return Encriptar(_mensaje.ObtenerContenido());
    }

    private string Encriptar(string contenido)
    {
        // Ejemplo simple de encriptación (en un caso real, usa un algoritmo de encriptación fuerte)
        char[] array = contenido.ToCharArray();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = (char)(array[i] + 1); // Cambia cada carácter por su sucesor
        }
        return new string(array);
    }
}
public class ComprimirMensaje : MensajeDecorador
{
    public ComprimirMensaje(Mensaje mensaje) : base(mensaje) { }

    public override string ObtenerContenido()
    {
        return Comprimir(_mensaje.ObtenerContenido());
    }

    private string Comprimir(string contenido)
    {
        // Ejemplo simple de compresión (en un caso real, usa un algoritmo de compresión como GZIP)
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(contenido));
    }
}
public class FirmarMensaje : MensajeDecorador
{
    public FirmarMensaje(Mensaje mensaje) : base(mensaje) { }

    public override string ObtenerContenido()
    {
        return _mensaje.ObtenerContenido() + " | Firma: " + Firmar(_mensaje.ObtenerContenido());
    }

    private string Firmar(string contenido)
    {
        // Ejemplo simple de firma (en un caso real, usa un algoritmo de hashing)
        return contenido.GetHashCode().ToString();
    }
}
class Program
{
    static void Main(string[] args)
    {
        Mensaje mensajeOriginal = new Mensaje("Hola, este es un mensaje de prueba.");

        Mensaje mensajeEncriptado = new EncriptarMensaje(mensajeOriginal);
        Mensaje mensajeComprimido = new ComprimirMensaje(mensajeEncriptado);
        Mensaje mensajeFirmado = new FirmarMensaje(mensajeComprimido);

        Console.WriteLine("Mensaje original: " + mensajeOriginal.ObtenerContenido());
        Console.WriteLine("Mensaje encriptado: " + mensajeEncriptado.ObtenerContenido());
        Console.WriteLine("Mensaje comprimido: " + mensajeComprimido.ObtenerContenido());
        Console.WriteLine("Mensaje firmado: " + mensajeFirmado.ObtenerContenido());
    }
}
