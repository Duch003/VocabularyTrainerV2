using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace UI.Models
{
    public class IOEngine
    {
        public bool IsValid(string path)
        {
            var serializer = new XmlSerializer(typeof(List<EntityModel>), path);
            
            XmlReader reader = XmlReader.Create(path);
            return serializer.CanDeserialize(reader);
        }

        public async Task<bool> Serialize(string filepath, List<EntityModel> list)
        {
            var output = true;
            var serializer = new XmlSerializer(typeof(List<EntityModel>));
            var stream = File.OpenWrite(filepath);
            try
            {
                await Task.Run(() => serializer.Serialize(stream, list));
            }
            catch (Exception)
            {
                output = false;
            }
            finally
            {
                stream.Close();
            }

            return output;
        }

        public async Task<List<EntityModel>> Deserialize(string filepath)
        {
            List<EntityModel> output;
            var deserializer = new XmlSerializer(typeof(List<EntityModel>));
            var stream = File.OpenRead(filepath);

            try
            {
                output = await Task.Run(() => (List<EntityModel>) deserializer.Deserialize(stream));
            }
            catch (Exception)
            {
                output = new List<EntityModel>();
            }
            finally
            {
                stream.Close();
            }

            return output;
        }
    }
}
