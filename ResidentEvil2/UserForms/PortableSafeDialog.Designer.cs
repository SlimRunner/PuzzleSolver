namespace ResidentEvil2.UserForms
{
    partial class PortableSafeDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.TabSolver = new System.Windows.Forms.TabPage();
            this.TabPractice = new System.Windows.Forms.TabPage();
            this.SolverTabLayout = new System.Windows.Forms.TableLayoutPanel();
            this.MainPanel.SuspendLayout();
            this.MainTabs.SuspendLayout();
            this.TabSolver.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.MainTabs);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(611, 432);
            this.MainPanel.TabIndex = 0;
            // 
            // MainTabs
            // 
            this.MainTabs.Controls.Add(this.TabSolver);
            this.MainTabs.Controls.Add(this.TabPractice);
            this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabs.Location = new System.Drawing.Point(0, 0);
            this.MainTabs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(611, 432);
            this.MainTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.MainTabs.TabIndex = 0;
            // 
            // TabSolver
            // 
            this.TabSolver.Controls.Add(this.SolverTabLayout);
            this.TabSolver.Location = new System.Drawing.Point(4, 30);
            this.TabSolver.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabSolver.Name = "TabSolver";
            this.TabSolver.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabSolver.Size = new System.Drawing.Size(603, 398);
            this.TabSolver.TabIndex = 0;
            this.TabSolver.Text = "Solver";
            this.TabSolver.UseVisualStyleBackColor = true;
            // 
            // TabPractice
            // 
            this.TabPractice.Location = new System.Drawing.Point(4, 30);
            this.TabPractice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPractice.Name = "TabPractice";
            this.TabPractice.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPractice.Size = new System.Drawing.Size(603, 398);
            this.TabPractice.TabIndex = 1;
            this.TabPractice.Text = "Practice";
            this.TabPractice.UseVisualStyleBackColor = true;
            // 
            // SolverTabLayout
            // 
            this.SolverTabLayout.ColumnCount = 2;
            this.SolverTabLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SolverTabLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SolverTabLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SolverTabLayout.Location = new System.Drawing.Point(4, 5);
            this.SolverTabLayout.Name = "SolverTabLayout";
            this.SolverTabLayout.RowCount = 2;
            this.SolverTabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SolverTabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.SolverTabLayout.Size = new System.Drawing.Size(595, 388);
            this.SolverTabLayout.TabIndex = 0;
            // 
            // PortableSafeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 432);
            this.Controls.Add(this.MainPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PortableSafeDialog";
            this.Text = "PortableSafeDialog";
            this.MainPanel.ResumeLayout(false);
            this.MainTabs.ResumeLayout(false);
            this.TabSolver.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.TabPage TabSolver;
        private System.Windows.Forms.TabPage TabPractice;
        private System.Windows.Forms.TableLayoutPanel SolverTabLayout;
    }
}