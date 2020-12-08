using System.IO;

namespace Gtt.Chess.Core.Pgn
{
    public static class PgnSevenTagRosterParser
    {
        public static PgnSevenTagRoster Parse(string pgnData)
        {
            PgnSevenTagRoster str = new PgnSevenTagRoster();

            using (StringReader sr = new StringReader(pgnData))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("["))
                    {
                        line = line.Replace("[", "").Replace("]", "");
                        string[] spsp = line.Split('"');
                        string label = spsp[0].Trim();
                        string value = "";
                        if (spsp.Length > 1)
                        {
                            value = spsp[1].Replace("\"", "");
                        }

                        switch (label.ToLower())
                        {
                            case "event":
                                str.Event = value;
                                break;
                            case "site":
                                str.Site = value;
                                break;
                            case "date":
                                str.Date = value;
                                break;
                            case "round":
                                str.Round = value;
                                break;
                            case "white":
                                str.White = value;
                                break;
                            case "black":
                                str.Black = value;
                                break;
                            case "result":
                                str.Result = value;
                                break;
                        }
                    }
                }
            }

            return str;
        }
    }
}