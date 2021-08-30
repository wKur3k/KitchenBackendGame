using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Entities
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ActionLeft { get; set; } = 5;
        public string Art { get; set; } = "iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAIAAABMXPacAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAAxBJREFUeF7tktGWpCAMROf/f7qHkZw9ro6IGFIVpu5To4CVuv31EVAkAIwEgJEAMBIARgLASAAYCQAjAWAkAIwEgGEX8HWBvc4P6SRW83XR9jq/CboBntb6dD8bXNGHq8zrgCj3yxKTOmAJ7VJfRgcUiR2LS+cAH9e9slwOwFlnlCUBD5hUViIHyKBTa8riQALASAAYWMqAglI4kAAwEgBmHQHnCyWgxVU7jq1JQIuAdiSgxVg7j05JQIthAf0Hxz4RDDLiQEFb/12nBi6HkExAPxJwj2NH56skoAuvmg73ZGm/QC2gs8ey7bCz8yAD+KCNsrZi7xMe9vQc4YEia7uy27f7De3NhLDEbRe3lfyDrTd+fWK/8kCU+La+rfD/sBcbh2UWuEKfa+0kafsFxtyP2tyUZW2/QBq91lqw9Ql7nbn6CvsAVvMJe52fNJOsVPoeCQAjAWAkAIwEgJEAMBIAJsFUpfo99nQVqOfZN77/8e/3ApBOcm75vDw8SQrXDLXWgq13XD2s2DohLNFve7x9295ACz50Z3ede3q2UQGLW8sq2PqORzsrtuYGkHKsnbEjA6eCictX6yjY+iFvDlZsTUZELJf5XW54f4k7cwM5zux4j9dVLkyJUocs2NoD99sqtsbhnGDeVPOunXRzJ27fnj3J7Mun3t/A4asx6WM+EfCVAxjtA8RXE4MEgJEAMBIARgLASAAYCQAjAWAkAIwEgJEAMBIARgLASAAYCQAjAWAkAIwEgJEAMDmmqu0v6UACwCQYad/7eg7Y5zk3vpgD6mGuul7JgQSA4Z2k3fIyDkjH6Ol3DQcSAIZxhv5mF3BAN8DTTrM74Eo/1mZqBxIAhij6mx7zOmDJ/b7BpA4kAAxFaK/uMjrAJ/ZtLZ0DcNwZfeVyIAFgkFnnNZXIASzo7I6yOMCkjGknhQMJAAOIGNkLv4PofPGNkDuQADCh4VBdMDuIS4ZtgdZBUCyG+TkdSAAYh0xlsFtsKxpL08S2RsH4p/hTSAAYCQAjAWAkAIwEgJEAMBIARgLASACUz+cb6w6aqjDXjUQAAAAASUVORK5CYII=";
        public int Gold { get; set; } = 40;
        public int Hp { get; set; } = 100;
        public int Atk { get; set; } = 0;
        public int Def { get; set; } = 0;
        public int Crit { get; set; } = 0;
        public int Speed { get; set; } = 0;

        public ICollection<Item> Items { get; set; }
        public virtual User User { get; set; }


    }
}
