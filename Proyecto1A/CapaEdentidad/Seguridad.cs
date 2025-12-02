using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1A.CapaEdentidad
{
    public class Seguridad
    {

        // Método estático que recibe un texto y devuelve su hash SHA-256.
        // No encripta: el hash NO puede revertirse, es una transformación irreversible.
        public static string Hash_SHA256(string texto)
        {
            // Se crea una instancia del algoritmo SHA256 dentro de un bloque "using".
            // El bloque using asegura que los recursos usados por SHA256 se liberen correctamente después de ejecutarse.
            using (SHA256 sha256 = SHA256.Create())
            {
                // 1. Convertimos el texto de entrada a un arreglo de bytes usando UTF-8.
                //    SHA-256 solo trabaja sobre bytes, no cadenas de texto.
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                // 2. Calculamos el hash usando el algoritmo SHA-256.
                // El resultado es un arreglo de 32 bytes (256 bits).
                byte[] hash = sha256.ComputeHash(bytes);
                // 3. Convertimos esos bytes en un string hexadecimal.
                //    ¿Por qué hexadecimal? porque es más legible y estándar en almacenamiento.
                StringBuilder sb = new StringBuilder();
                // Recorremos cada byte del hash calculado
                foreach (byte b in hash)
                    // Convertimos cada byte a su representación en dos dígitos hexadecimales.
                    // Ejemplo: 15 -> "0f", 255 -> "ff"
                    sb.Append(b.ToString("x2"));

                // 4. Retornamos el hash final como una cadena de 64 caracteres (hexadecimal).
                return sb.ToString();
            }
        }


    }
}
