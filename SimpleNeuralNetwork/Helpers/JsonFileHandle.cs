﻿using Newtonsoft.Json;
using SimpleNeuralNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Helpers
{
    public class JsonFileHandle : IFileHandle
    {
        private string _folder;

        public JsonFileHandle(string folder)
        {
            _folder = folder;
        }

        public void Write<T>(string fileName, T obj, bool append = false) where T : new()
        {
            TextWriter writer = null;

            var json = JsonConvert.SerializeObject(obj, Formatting.Indented,
                                                        new JsonSerializerSettings
                                                        {
                                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                        });
            writer = new StreamWriter(_folder + Path.DirectorySeparatorChar + fileName, append);
            writer.Write(json);

            writer.Close();

        }
        public T Read<T>(string fileName) where T : new()
        {
            TextReader reader = null;
            reader = new StreamReader(_folder + Path.DirectorySeparatorChar + fileName);
            var json = reader.ReadToEnd();
            reader.Close();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}