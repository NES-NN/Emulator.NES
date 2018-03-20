namespace dotNES
{
    partial class SuperMarioBros
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxInputs = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelPowerUP = new System.Windows.Forms.Label();
            this.labelPlayerState = new System.Windows.Forms.Label();
            this.labelLevelX = new System.Windows.Forms.Label();
            this.labelLevelY = new System.Windows.Forms.Label();
            this.labelScreenX = new System.Windows.Forms.Label();
            this.labelScreenY = new System.Windows.Forms.Label();
            this.labelDirection = new System.Windows.Forms.Label();
            this.labelFloatState = new System.Windows.Forms.Label();
            this.state = new System.Windows.Forms.Label();
            this.powerUP = new System.Windows.Forms.Label();
            this.levelX = new System.Windows.Forms.Label();
            this.levelY = new System.Windows.Forms.Label();
            this.screenX = new System.Windows.Forms.Label();
            this.screenY = new System.Windows.Forms.Label();
            this.direction = new System.Windows.Forms.Label();
            this.floatState = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelLives = new System.Windows.Forms.Label();
            this.labelCoins = new System.Windows.Forms.Label();
            this.labelWorld = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.score = new System.Windows.Forms.Label();
            this.lives = new System.Windows.Forms.Label();
            this.coins = new System.Windows.Forms.Label();
            this.world = new System.Windows.Forms.Label();
            this.level = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.refresh = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInputs)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(549, 203);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxInputs);
            this.groupBox1.Location = new System.Drawing.Point(369, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 192);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inputs";
            // 
            // pictureBoxInputs
            // 
            this.pictureBoxInputs.Location = new System.Drawing.Point(7, 20);
            this.pictureBoxInputs.Name = "pictureBoxInputs";
            this.pictureBoxInputs.Size = new System.Drawing.Size(162, 162);
            this.pictureBoxInputs.TabIndex = 0;
            this.pictureBoxInputs.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(176, 192);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PlayerStats";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.labelPowerUP, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelPlayerState, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelLevelX, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelLevelY, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelScreenX, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.labelScreenY, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.labelDirection, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.labelFloatState, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.state, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.powerUP, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.levelX, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.levelY, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.screenX, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.screenY, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.direction, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.floatState, 1, 7);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(165, 165);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // labelPowerUP
            // 
            this.labelPowerUP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPowerUP.AutoSize = true;
            this.labelPowerUP.Location = new System.Drawing.Point(18, 20);
            this.labelPowerUP.Name = "labelPowerUP";
            this.labelPowerUP.Size = new System.Drawing.Size(61, 13);
            this.labelPowerUP.TabIndex = 1;
            this.labelPowerUP.Text = "Power UP :";
            // 
            // labelPlayerState
            // 
            this.labelPlayerState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlayerState.AutoSize = true;
            this.labelPlayerState.Location = new System.Drawing.Point(9, 0);
            this.labelPlayerState.Name = "labelPlayerState";
            this.labelPlayerState.Size = new System.Drawing.Size(70, 13);
            this.labelPlayerState.TabIndex = 0;
            this.labelPlayerState.Text = "Player State :";
            // 
            // labelLevelX
            // 
            this.labelLevelX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLevelX.AutoSize = true;
            this.labelLevelX.Location = new System.Drawing.Point(30, 40);
            this.labelLevelX.Name = "labelLevelX";
            this.labelLevelX.Size = new System.Drawing.Size(49, 13);
            this.labelLevelX.TabIndex = 2;
            this.labelLevelX.Text = "Level X :";
            // 
            // labelLevelY
            // 
            this.labelLevelY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLevelY.AutoSize = true;
            this.labelLevelY.Location = new System.Drawing.Point(30, 60);
            this.labelLevelY.Name = "labelLevelY";
            this.labelLevelY.Size = new System.Drawing.Size(49, 13);
            this.labelLevelY.TabIndex = 3;
            this.labelLevelY.Text = "Level Y :";
            // 
            // labelScreenX
            // 
            this.labelScreenX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScreenX.AutoSize = true;
            this.labelScreenX.Location = new System.Drawing.Point(22, 80);
            this.labelScreenX.Name = "labelScreenX";
            this.labelScreenX.Size = new System.Drawing.Size(57, 13);
            this.labelScreenX.TabIndex = 4;
            this.labelScreenX.Text = "Screen X :";
            // 
            // labelScreenY
            // 
            this.labelScreenY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScreenY.AutoSize = true;
            this.labelScreenY.Location = new System.Drawing.Point(22, 100);
            this.labelScreenY.Name = "labelScreenY";
            this.labelScreenY.Size = new System.Drawing.Size(57, 13);
            this.labelScreenY.TabIndex = 5;
            this.labelScreenY.Text = "Screen Y :";
            // 
            // labelDirection
            // 
            this.labelDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDirection.AutoSize = true;
            this.labelDirection.Location = new System.Drawing.Point(24, 120);
            this.labelDirection.Name = "labelDirection";
            this.labelDirection.Size = new System.Drawing.Size(55, 13);
            this.labelDirection.TabIndex = 6;
            this.labelDirection.Text = "Direction :";
            // 
            // labelFloatState
            // 
            this.labelFloatState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFloatState.AutoSize = true;
            this.labelFloatState.Location = new System.Drawing.Point(15, 140);
            this.labelFloatState.Name = "labelFloatState";
            this.labelFloatState.Size = new System.Drawing.Size(64, 13);
            this.labelFloatState.TabIndex = 7;
            this.labelFloatState.Text = "Float State :";
            // 
            // state
            // 
            this.state.AutoSize = true;
            this.state.Location = new System.Drawing.Point(85, 0);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(30, 13);
            this.state.TabIndex = 8;
            this.state.Text = "state";
            // 
            // powerUP
            // 
            this.powerUP.AutoSize = true;
            this.powerUP.Location = new System.Drawing.Point(85, 20);
            this.powerUP.Name = "powerUP";
            this.powerUP.Size = new System.Drawing.Size(51, 13);
            this.powerUP.TabIndex = 9;
            this.powerUP.Text = "powerUP";
            // 
            // levelX
            // 
            this.levelX.AutoSize = true;
            this.levelX.Location = new System.Drawing.Point(85, 40);
            this.levelX.Name = "levelX";
            this.levelX.Size = new System.Drawing.Size(36, 13);
            this.levelX.TabIndex = 10;
            this.levelX.Text = "levelX";
            // 
            // levelY
            // 
            this.levelY.AutoSize = true;
            this.levelY.Location = new System.Drawing.Point(85, 60);
            this.levelY.Name = "levelY";
            this.levelY.Size = new System.Drawing.Size(36, 13);
            this.levelY.TabIndex = 11;
            this.levelY.Text = "levelY";
            // 
            // screenX
            // 
            this.screenX.AutoSize = true;
            this.screenX.Location = new System.Drawing.Point(85, 80);
            this.screenX.Name = "screenX";
            this.screenX.Size = new System.Drawing.Size(46, 13);
            this.screenX.TabIndex = 12;
            this.screenX.Text = "screenX";
            // 
            // screenY
            // 
            this.screenY.AutoSize = true;
            this.screenY.Location = new System.Drawing.Point(85, 100);
            this.screenY.Name = "screenY";
            this.screenY.Size = new System.Drawing.Size(46, 13);
            this.screenY.TabIndex = 13;
            this.screenY.Text = "screenY";
            // 
            // direction
            // 
            this.direction.AutoSize = true;
            this.direction.Location = new System.Drawing.Point(85, 120);
            this.direction.Name = "direction";
            this.direction.Size = new System.Drawing.Size(47, 13);
            this.direction.TabIndex = 14;
            this.direction.Text = "direction";
            // 
            // floatState
            // 
            this.floatState.AutoSize = true;
            this.floatState.Location = new System.Drawing.Point(85, 140);
            this.floatState.Name = "floatState";
            this.floatState.Size = new System.Drawing.Size(52, 13);
            this.floatState.TabIndex = 15;
            this.floatState.Text = "floatState";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel3);
            this.groupBox3.Location = new System.Drawing.Point(186, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 192);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Game Stats";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.labelScore, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelLives, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelCoins, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.labelWorld, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.labelLevel, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.labelTime, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.score, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lives, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.coins, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.world, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.level, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.time, 1, 5);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(7, 20);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(164, 122);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // labelScore
            // 
            this.labelScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScore.AutoSize = true;
            this.labelScore.Location = new System.Drawing.Point(38, 0);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(41, 13);
            this.labelScore.TabIndex = 0;
            this.labelScore.Text = "Score :";
            // 
            // labelLives
            // 
            this.labelLives.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLives.AutoSize = true;
            this.labelLives.Location = new System.Drawing.Point(41, 20);
            this.labelLives.Name = "labelLives";
            this.labelLives.Size = new System.Drawing.Size(38, 13);
            this.labelLives.TabIndex = 1;
            this.labelLives.Text = "Lives :";
            // 
            // labelCoins
            // 
            this.labelCoins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCoins.AutoSize = true;
            this.labelCoins.Location = new System.Drawing.Point(40, 40);
            this.labelCoins.Name = "labelCoins";
            this.labelCoins.Size = new System.Drawing.Size(39, 13);
            this.labelCoins.TabIndex = 2;
            this.labelCoins.Text = "Coins :";
            // 
            // labelWorld
            // 
            this.labelWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWorld.AutoSize = true;
            this.labelWorld.Location = new System.Drawing.Point(38, 60);
            this.labelWorld.Name = "labelWorld";
            this.labelWorld.Size = new System.Drawing.Size(41, 13);
            this.labelWorld.TabIndex = 3;
            this.labelWorld.Text = "World :";
            // 
            // labelLevel
            // 
            this.labelLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(40, 80);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(39, 13);
            this.labelLevel.TabIndex = 4;
            this.labelLevel.Text = "Level :";
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(43, 100);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(36, 13);
            this.labelTime.TabIndex = 5;
            this.labelTime.Text = "Time :";
            // 
            // score
            // 
            this.score.AutoSize = true;
            this.score.Location = new System.Drawing.Point(85, 0);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(33, 13);
            this.score.TabIndex = 6;
            this.score.Text = "score";
            // 
            // lives
            // 
            this.lives.AutoSize = true;
            this.lives.Location = new System.Drawing.Point(85, 20);
            this.lives.Name = "lives";
            this.lives.Size = new System.Drawing.Size(28, 13);
            this.lives.TabIndex = 7;
            this.lives.Text = "lives";
            // 
            // coins
            // 
            this.coins.AutoSize = true;
            this.coins.Location = new System.Drawing.Point(85, 40);
            this.coins.Name = "coins";
            this.coins.Size = new System.Drawing.Size(32, 13);
            this.coins.TabIndex = 8;
            this.coins.Text = "coins";
            // 
            // world
            // 
            this.world.AutoSize = true;
            this.world.Location = new System.Drawing.Point(85, 60);
            this.world.Name = "world";
            this.world.Size = new System.Drawing.Size(32, 13);
            this.world.TabIndex = 9;
            this.world.Text = "world";
            // 
            // level
            // 
            this.level.AutoSize = true;
            this.level.Location = new System.Drawing.Point(85, 80);
            this.level.Name = "level";
            this.level.Size = new System.Drawing.Size(29, 13);
            this.level.TabIndex = 10;
            this.level.Text = "level";
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Location = new System.Drawing.Point(85, 100);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(26, 13);
            this.time.TabIndex = 11;
            this.time.Text = "time";
            // 
            // refresh
            // 
            this.refresh.Tick += new System.EventHandler(this.refresh_Tick);
            // 
            // SuperMarioBros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 220);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SuperMarioBros";
            this.Text = "SuperMarioBros";
            this.Load += new System.EventHandler(this.SuperMarioBros_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInputs)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBoxInputs;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelPowerUP;
        private System.Windows.Forms.Label labelPlayerState;
        private System.Windows.Forms.Label labelLevelX;
        private System.Windows.Forms.Label labelLevelY;
        private System.Windows.Forms.Label labelScreenX;
        private System.Windows.Forms.Label labelScreenY;
        private System.Windows.Forms.Label labelDirection;
        private System.Windows.Forms.Label labelFloatState;
        private System.Windows.Forms.Label state;
        private System.Windows.Forms.Label powerUP;
        private System.Windows.Forms.Label levelX;
        private System.Windows.Forms.Label levelY;
        private System.Windows.Forms.Label screenX;
        private System.Windows.Forms.Label screenY;
        private System.Windows.Forms.Label direction;
        private System.Windows.Forms.Label floatState;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelLives;
        private System.Windows.Forms.Label labelCoins;
        private System.Windows.Forms.Label labelWorld;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label score;
        private System.Windows.Forms.Label lives;
        private System.Windows.Forms.Label coins;
        private System.Windows.Forms.Label world;
        private System.Windows.Forms.Label level;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Timer refresh;
    }
}