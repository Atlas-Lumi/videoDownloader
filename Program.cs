using System;
using System.IO;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Videos;

public class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor= ConsoleColor.Cyan;
        var youtube = new YoutubeClient();
        Console.WriteLine("Enter the url of the video you want to download: ");
        var url = Console.ReadLine();
        var video = youtube.Videos.GetAsync(url).Result;
        var streamManifest = youtube.Videos.Streams.GetManifestAsync(video.Id).Result;
        var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
        var stream = youtube.Videos.Streams.GetAsync(streamInfo).Result;
        var file = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads/video.mp4");
        stream.CopyTo(file);
        file.Close();

        Console.WriteLine("The video will be in your downloads folder. Press any key to exit.");
        Console.ReadKey();
    }
}