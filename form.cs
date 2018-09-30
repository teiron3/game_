using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

///<summary>form の形状とイベントを記載</summary>
partial class form : Form{

    ///<summary>停止フラグ</summary>
    bool stop_flg = false;
    
    ///<summary>csvファイルのデータを格納する</summary>
    ///<remarks>Dictionary クラスを宣言</remarks>
    public Dictionary<string, pic_data_class> p_class = new Dictionary<string, pic_data_class>();

    ///<summary>csvファイル名の設定</summary>
    public string csv_file{get{return "csv_file.csv";}}

    ///<summary>ダメージ判定用フラグ</summary>
    int damageRed = 0,damageOrange = 0;

    ///<summary>入渠判定フラグ</summary>
    ///<remarks>true のときに入渠</remarks>
    bool dockflg = false;

    ///<summary>戦闘海域周回継続かどうかのフラグ</summary>
    ///<remarks>true で継続終了</remarks>
    bool continueflg = false;

    //要素のクラス宣言とインスタンス化
    Button btn1 = new Button();
    Button btn2 = new Button();
    Button btn3 = new Button();
    RadioButton rbtn1 = new RadioButton();
    RadioButton rbtn2 = new RadioButton();
    RadioButton rbtn3 = new RadioButton();
    RadioButton rbtn4 = new RadioButton();

    ///<summary>コントラクタ(形状の設定)</summary>
    public form(){
        // formの大きさとタイトルを設定
        this.Text = "game manupirate";
        this.Size = new Size(1200, 200);
        this.StartPosition = FormStartPosition.Manual;
        this.Location = new Point(0, 730);

        //各種要素を設定

        //ボタン
        btn1.Parent = this;
        btn1.Location = new Point(15, 90);
        btn1.Size = new Size(90, 40);
        btn1.Text = "スタート";
        btn1.Click += new EventHandler(btn1_action_start);

        btn2.Parent = this;
        btn2.Location = new Point(120, 90);
        btn2.Size = new Size(90, 40);
        btn2.Text = "停止ボタン";
        btn2.Click += new EventHandler(btn2_end_key);

        btn3.Parent = this;
        btn3.Location = new Point(240, 90);
        btn3.Size = new Size(90, 40);
        btn3.Text = "ウィンドウ位置修正";
        btn3.Click += new EventHandler(btn3_window);

        //ラジオボタン
        rbtn1.Parent = this;
        rbtn1.Location = new Point(15, 20);
        rbtn1.Text = "遠征のみ";

        rbtn2.Parent = this;
        rbtn2.Location = new Point(140, 20);
        rbtn2.Text = "test";

        rbtn3.Parent = this;
        rbtn3.Location = new Point(15, 55);
        rbtn3.Text = "";

        rbtn4.Parent = this;
        rbtn4.Location = new Point(140, 55);
        rbtn4.Text = "";
    }

    //イベントの設定
    //btn1 のイベント(action start)
    private async void btn1_action_start(object sender, EventArgs e){
        btn1.Enabled = false;
        if(rbtn1.Checked == true){
            await Task.Run(()=> expendjitiononly());
            btn2.Text = "停止ボタン";
            btn1.Enabled = true;
            stop_flg = false;
            return;
        }

        if(rbtn2.Checked == true){
            Task task = Task.Run(()=> plantest());
            await task;
            btn2.Text = "停止ボタン";
            btn1.Enabled = true;
            stop_flg = false;
            return;
        }
        /*
        if(rbtn3.Checked == true){
            Task.Run(()=> testmeso(this.x));
            return;
        }
        if(rbtn4.Checked == true){
            Task.Run(()=> testmeso(this.x));
            return;
        }
         */
        
    }

    //btn2のイベント(action stop)
    void btn2_end_key(object sender, EventArgs e){
        btn2.Text = "停止処理中";
        this.stop_flg = true;
    }

    //ウインドウ位置の修正処理発動ボタンイベント
    void btn3_window(object sender, EventArgs e){
        new handmade.operation_window();
    }
    
    
}
