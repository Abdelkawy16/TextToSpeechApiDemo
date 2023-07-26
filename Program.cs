using Google.Cloud.TextToSpeech.V1;
using System;

namespace TextToSpeechApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", (Directory.GetCurrentDirectory()) + "\\key.json");

            var client = TextToSpeechClient.Create();

            // read .txt file
            string text = "This is a demonstration of the Google Cloud Text-to-Speech API";
            if (File.Exists((Directory.GetCurrentDirectory()) + "\\input.txt"))
            {
                text = System.IO.File.ReadAllText((Directory.GetCurrentDirectory()) + "\\input.txt");
            }

            // The input to be synthesized, can be provided as text or SSML.
            var input = new SynthesisInput
            {
                Text = text
            };

            // Build the voice request.
            var voiceSelection = new VoiceSelectionParams
            {
                LanguageCode = "en-US",
                SsmlGender = SsmlVoiceGender.Male
            };

            // Specify the type of audio file.
            var audioConfig = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Mp3
            };

            // Perform the text-to-speech request.
            var response = client.SynthesizeSpeech(input, voiceSelection, audioConfig);

            // Write the response to the output file.
            var outputPath = (Directory.GetCurrentDirectory()) + "\\output.mp3";
            using (var output = File.Create(outputPath))
            {
                response.AudioContent.WriteTo(output);
            }
            Console.WriteLine($"Audio content written to file \"{outputPath}\"");
        }
    }
}