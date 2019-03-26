using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;

namespace html_agility
{
    class Program
    {
        private static String StateName;
        private static String DistrictName;
        private static String BlockName;

        static void Main(string[] args)
        {

            string path = @"D:\html-agility\html";

            string[] filePaths = Directory.GetFiles(path , "*.html");

            foreach (var file in filePaths)
            {
                var doc = new HtmlDocument();

                doc.Load(file);

                var node = doc.DocumentNode.SelectNodes("//table[contains(@class, 'table-rpt-report')]").Descendants("tr");

                foreach (HtmlNode item in node)
                {
                    TraverseNode(item);
                }

            }
        }

        private static void TraverseNode(HtmlNode item)
        {
            var itemData = item.InnerHtml;
            HtmlNodeCollection hmc = item.ChildNodes;
            foreach (var tr in hmc)
            {
                if (tr.InnerHtml.Trim().Length != 0)
                {
                    foreach (var span in tr.ChildNodes)
                    {
                        String[] data = span.InnerText.Split(':');
                        if (data.Length > 1)
                        {
                            Console.WriteLine(data[0].Trim());
                            Console.WriteLine(data[1].Trim());
                            StateName = data[1].Trim();
                        }
                    }
                };
            }
        }
    }
}
