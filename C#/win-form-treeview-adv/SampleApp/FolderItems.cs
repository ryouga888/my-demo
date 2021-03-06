using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace SampleApp
{
    public abstract class BaseItem
    {
        private string _path = "";

        public string ItemPath
        {
            get { return _path; }
            set { _path = value; }
        }

        private Image _icon;

        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        private string _size = "";

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private string _date = "";

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public abstract string Name { get; set; }

        private BaseItem _parent;

        public BaseItem Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        private double _process;

        public Double Process
        {
            get { return _process; }
            set { _process = value; }
        }

        public Color ProcessColor
        {
            get
            {
                if (_process > 80)
                {
                    return Color.Red;
                }

                if (_process > 60)
                {
                    return Color.Blue;
                }

                if (_process > 20)
                {
                    return Color.Aqua;
                }

                return Color.AntiqueWhite;
            }
        }

        public bool ProcessHide { get; set; }
    }

    public class RootItem : BaseItem
    {
        public RootItem(string name)
        {
            ItemPath = name;
        }

        public override string Name
        {
            get { return ItemPath; }
            set { }
        }
    }

    public class FolderItem : BaseItem
    {
        public override string Name
        {
            get { return Path.GetFileName(ItemPath); }
            set
            {
                string dir = Path.GetDirectoryName(ItemPath);
                string destination = Path.Combine(dir, value);
                Directory.Move(ItemPath, destination);
                ItemPath = destination;
            }
        }

        public FolderItem(string name, BaseItem parent)
        {
            ItemPath = name;
            Parent = parent;
        }
    }

    public class FileItem : BaseItem
    {
        public override string Name
        {
            get { return Path.GetFileName(ItemPath); }
            set
            {
                string dir = Path.GetDirectoryName(ItemPath);
                string destination = Path.Combine(dir, value);
                File.Move(ItemPath, destination);
                ItemPath = destination;
            }
        }

        public FileItem(string name, BaseItem parent)
        {
            ItemPath = name;
            Parent = parent;
        }
    }
}