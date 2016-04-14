using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDocumentDB
{
    class Program
    {
        const string ENDPOINT_URL = "";
        const string AUTHORIZATION_KEY = "";
        const string DATABASE_NAME = "";
        const string COLLECTION_NAME = "";
        static DocumentClient cliente;

        static void Main(string[] args)
        {
        
            
            try
            {
                cliente = new DocumentClient(new Uri(ENDPOINT_URL), AUTHORIZATION_KEY);

                Persona persona = new Persona()
                {
                    Nombre = "Carlos",
                    Edad = 21
                };

                //agregarDocumento(persona);
                listarDocumentos();

            }
            catch (Exception e)
            {

                Console.WriteLine("Error: " + e.ToString());
            }
            
            Console.ReadLine();


        }

        private async static void agregarDocumento(Persona persona)
        {
            try
            {

                await cliente.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DATABASE_NAME, COLLECTION_NAME), persona);

                Console.WriteLine("Documento creado");

            }
            catch (Exception e)
            {

                Console.WriteLine("Error: " + e.ToString());
            }
        }

        private async static void listarDocumentos()
        {

            var documentos = await cliente.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri(DATABASE_NAME, COLLECTION_NAME), new FeedOptions { MaxItemCount = 10 });
            foreach (var documento in documentos)
            {
                Console.WriteLine(documento);
            }
            
        }

        private static void consultarDocumentos()
        {
            // Ejecutamos la consulta SQL
            IQueryable<Persona> familyQueryInSql = cliente.CreateDocumentQuery<Persona>(
                UriFactory.CreateDocumentCollectionUri(DATABASE_NAME, COLLECTION_NAME),
                "SELECT * FROM Persona ");

            Console.WriteLine("Running direct SQL query...");
            foreach (Persona persona in familyQueryInSql)
            {
                Console.WriteLine("\tRead {0}", persona.Nombre);
            }
        }

        

    }
}
