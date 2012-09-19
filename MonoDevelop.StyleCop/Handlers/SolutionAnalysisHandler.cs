﻿//-----------------------------------------------------------------------
// <copyright file="SolutionAnalysisHandler.cs">
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
  using MonoDevelop.Components.Commands;
  using MonoDevelop.Ide;

  /// <summary>
  /// Class which handles the analysis type Solution.
  /// </summary>
  internal sealed class SolutionAnalysisHandler : BaseAnalysisHandler
  {
    #region Protected Override Methods

    /// <summary>
    /// Starts a full StyleCop analysis of type File.
    /// </summary>
    protected override void Run()
    {
      base.Run();

      this.FullAnalysis = true;
      this.Analyze(AnalysisType.Solution);
    }

    /// <summary>
    /// Update availability of the StyleCop command for the selected solution in ProjectPad.
    /// </summary>
    /// <param name="info">A <see cref="CommandInfo"/></param>
    protected override void Update(CommandInfo info)
    {
      // TODO correct this check! We should also go through all projects of the solution to make sure it has compatible projects.
      if (IdeApp.Workspace.IsOpen && IdeApp.ProjectOperations.CurrentRunOperation.IsCompleted)
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