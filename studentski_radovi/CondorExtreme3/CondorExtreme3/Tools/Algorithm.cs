using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CondorExtreme3.Tools
{
    public static class Algorithm
    {
        public static void ContextSort(this List<string> list, string item)
        {
            List<string> tmp = new List<string>();
            List<string> deleted = list.Cast<string>().ToList();

            string line = "";
            for (int i = 0; i < list.Count; i++)
            {
                line = ContextFind(deleted, item);
                tmp.Add(line);
                deleted.Remove(line);
            }

            list.Clear();
            list.AddRange(tmp.Cast<string>().ToArray());
        }

        //Finds the most similar item using Context Similarity algorithm. Works only for lists of strings
        public static string ContextFind<T>(this List<T> list, string item)
        {
            return list.Cast<string>().ToList()[GetBestMatch(item, list.Cast<string>().ToList())];
        }

        // SimilarityCompare algorithm
        // Returns how similar string1 is to string2
        public static decimal SCompare(string pattern1, string pattern2)
        {
            int cardS1 = pattern1.Length;
            int cardS2 = pattern2.Length;

            pattern1 = pattern1.ToLower();
            pattern2 = pattern2.ToLower();

            decimal totalScore = 0.0m;

            for (int i = 0; i <= cardS1; i++)
            {
                for (int j = 0; j <= cardS1 - i; j++)
                {
                    if (pattern2.Contains(pattern1.Substring(i, j)))
                        totalScore += pattern1.Substring(i, j).Length;
                    else break;
                }
            }

            decimal normalization = (cardS1 * (cardS1 + 1) * (cardS1 + 2)) / 6.0m;
            return (totalScore / normalization);
        }

        // Find the pattern in arrayOfPatterns that best matches searchPattern
        public static int GetBestMatch(string searchPattern, List<string> arrayOfPatterns)
        {
            int bestIndex = 0;
            decimal bestScore = SCompare(searchPattern, arrayOfPatterns[bestIndex]);
            decimal tmpScore = 0.0m;

            for (int i = 1; i < arrayOfPatterns.Count; i++)
            {
                tmpScore = SCompare(searchPattern, arrayOfPatterns[i]);

                if (bestScore < tmpScore)
                {
                    bestIndex = i;
                    bestScore = tmpScore;
                }
            }

            return bestIndex;
        }

        // Gives the first {linkCount} number of suggested {movieTitle} trailer links
        
        public static List<string> SuggestTrailerLinks(string movieTitle, int linkCount)
        {
            if(movieTitle == null)
             return new List<string>();

            HtmlWeb website = new HtmlWeb();
            HtmlDocument rootDocument = null;
            List<string> links = new List<string>();

            string searchQuery = movieTitle.Trim() + " Trailer";
            string searchLink = string.Format("https://www.google.com/search?q={0}&num=10&start=0", 
                searchQuery.Replace(" ", "+"));

            rootDocument = website.Load(searchLink);
            string t = rootDocument.ToString();
            var nodes = rootDocument.DocumentNode.SelectNodes("//cite");

            for (int i = 0; i < nodes.Count && i < linkCount; i++)
                links.Add(nodes[i].InnerText.Replace("watch?v=", "embed/"));

            return links;
        }

        public static string GetStringSha256Hash(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }
}