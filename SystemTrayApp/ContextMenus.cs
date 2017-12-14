using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using SystemTrayApp.Properties;
using System.Drawing;

namespace SystemTrayApp
{
	class ContextMenus
	{
		bool isAboutLoaded = false;
        public static string[] Directories = Directory.GetDirectories(ProcessIcon.dirDocuments + @"\Matek Tray Utility", "*.*", System.IO.SearchOption.TopDirectoryOnly);

        public ContextMenuStrip Create()
		{   

            // Add the default menu options.
            ContextMenuStrip menu = new ContextMenuStrip();
			ToolStripMenuItem item;
            ToolStripSeparator sep;
            bool placeSep = false;

            for (int y = 0, ylength = Directories.Length; y < ylength; y++)
            {
                string[] fileEntries = Directory.GetFiles(Directories[y]);
                foreach (string s in fileEntries)
                {
                    if (fileEntries.Length > 0)
                    {
                        item = new ToolStripMenuItem();
                        item.Image = Icon.ExtractAssociatedIcon(s).ToBitmap();
                        item.Text = Path.GetFileNameWithoutExtension(s);
                        item.Tag = s;
                        item.Click += new EventHandler(RDP_Click);
                        menu.Items.Add(item);
                        placeSep = true;
                    }
                    else
                    {
                        placeSep = false;
                    }
                }
                if (placeSep == true)
                {
                    // Separator.
                    sep = new ToolStripSeparator();
                    menu.Items.Add(sep);
                }
            }
            

            // Windows Explorer.
            item = new ToolStripMenuItem();
			item.Text = "Explorer";
			item.Click += new EventHandler(Explorer_Click);
			item.Image = Resources.Explorer;
			menu.Items.Add(item);


			// About.
			item = new ToolStripMenuItem();
			item.Text = "About";
			item.Click += new EventHandler(About_Click);
			item.Image = Resources.About;
			menu.Items.Add(item);

			// Separator.
			sep = new ToolStripSeparator();
			menu.Items.Add(sep);

            // Exit.
            item = new ToolStripMenuItem();
			item.Text = "Exit";
			item.Click += new System.EventHandler(Exit_Click);
			item.Image = Resources.Exit;
			menu.Items.Add(item);

			return menu;
		}

        /// <summary>
        /// Handles the Click event of the Explorer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Explorer_Click(object sender, EventArgs e)
		{
			Process.Start("explorer", null);
		}

        /// <summary>
        /// Handles the Click event of the Explorer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Settings_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", null);
        }

        /// <summary>
        /// Handles the Click event of the About control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void About_Click(object sender, EventArgs e)
		{
			if (!isAboutLoaded)
			{
				isAboutLoaded = true;
				new AboutBox().ShowDialog();
				isAboutLoaded = false;
			}
		}

        /// <summary>
        /// Handles the Click event of the RDP control.
        /// </summary>
        /// <param name="filePath">Path of file</param>		
        /// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void RDP_Click(object sender, EventArgs e)
        {
            string filePath = (string)(((ToolStripMenuItem)sender).Tag);
            Process.Start(filePath, null);
        }

		/// <summary>
		/// Processes a menu item.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Exit_Click(object sender, EventArgs e)
		{
			// Quit without further ado.
			Application.Exit();
		}
	}
}