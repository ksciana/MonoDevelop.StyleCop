﻿//-----------------------------------------------------------------------
// <copyright file="StyleCopSettingsHandler.cs">
//   APL 2.0
// </copyright>
// <license>
//   Copyright 2012 Alexander Jochum
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </license>
//-----------------------------------------------------------------------
namespace MonoDevelop.StyleCop
{
  using System.IO;
  using MonoDevelop.Components.Commands;
  using MonoDevelop.Ide;
  using global::StyleCop;

  /// <summary>
  /// Class which handles the StyleCop settings dialog.
  /// </summary>
  internal class StyleCopSettingsHandler : CommandHandler
  {
    #region Protected Override Methods

    /// <summary>
    /// Show StyleCop settings dialog.
    /// </summary>
    protected override void Run()
    {
      base.Run();

      var selectedProject = IdeApp.ProjectOperations.CurrentSelectedProject;
      if (selectedProject != null && !string.IsNullOrEmpty(selectedProject.BaseDirectory))
      {
        // Show StyleCops local settings dialog.
        string settingsFilePath = Path.Combine(selectedProject.BaseDirectory, Settings.DefaultFileName);
        if (!File.Exists(settingsFilePath))
        {
          string deprecatedFilePath = Path.Combine(selectedProject.BaseDirectory, Settings.AlternateFileName);
          if (!File.Exists(deprecatedFilePath))
          {
            deprecatedFilePath = Path.Combine(selectedProject.BaseDirectory, V101Settings.DefaultFileName);
            if (File.Exists(deprecatedFilePath))
            {
              settingsFilePath = deprecatedFilePath;
            }
          }
          else
          {
            settingsFilePath = deprecatedFilePath;
          }
        }

        ProjectUtilities.Instance.Core.ShowSettings(settingsFilePath);
      }
    }

    /// <summary>
    /// Update visibility of the StyleCop settings command for the selected project/projects in ProjectPad.
    /// </summary>
    /// <param name="info">A <see cref="CommandInfo"/></param>
    protected override void Update(CommandInfo info)
    {
      base.Update(info);

      if (ProjectUtilities.Instance.IsKnownProjectType(IdeApp.ProjectOperations.CurrentSelectedProject))
      {
        info.Visible = true;
      }
      else
      {
        info.Visible = false;
      }
    }

    #endregion Protected Override Methods
  }
}