using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

// form の形状とイベントを記載
partial class form : Form{

    //停止フラグ
    bool stop_flg = false;
    // csvファイルのデータを格納する Dictionary クラスを宣言
    public Dictionary<string, pic_data_class> p_class = new Dictionary<string, pic_data_class>();
    // csvファイル名の設定
    public string csv_file{get{return "csv_file.csv";}}
    //ダメージ判定用フラグ
    int damageRed = 0,damageOrange = 0;

    //要素のクラス宣言とインスタンス化
    Button btn1 = new Button();
    Button btn2 = new Button();
    Button btn3 = new Button();
    RadioButton rbtn1 = new RadioButton();
    RadioButton rbtn2 = new RadioButton();
    RadioButton rbtn3 = new RadioButton();
    RadioButton rbtn4 = new RadioButton();

    //コントラクタ(形状の設定)
    public form(){
        // formの大きさとタイトルを設定
        this.Text = "game manupirate";
        this.Size = new Size(1200, 200);
        this.StartPosition = FormStartPosition.Manual;
        this.Location = new Point(0, 730);

        //各種要素を設定

        //ボタン
        btn1.Parent = this;
        btn1.Location = new Point(50, 150);
        btn1.Text = "スタート";
        btn1.Click += new EventHandler(btn1_action_start);

        btn2.Parent = this;
        btn2.Location = new Point(50, 190);
        btn2.Text = "停止ボタン";
        btn2.Click += new EventHandler(btn2_end_key);

        btn3.Parent = this;
        btn3.Location = new Point(50, 230);
        btn3.Text = "ウィンドウ位置修正";
        btn3.Click += new EventHandler(btn3_window);

        //ラジオボタン
        rbtn1.Parent = this;
        rbtn1.Location = new Point(15, 20);
        rbtn1.Text = "遠征のみ";

        rbtn2.Parent = this;
        rbtn2.Location = new Point(15, 40);
        rbtn2.Text = "test";

        rbtn3.Parent = this;
        rbtn3.Location = new Point(15, 60);
        rbtn3.Text = "";

        rbtn4.Parent = this;
        rbtn4.Location = new Point(15, 80);
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
            return;
        }

        if(rbtn2.Checked == true){
            Task task = Task.Run(()=> damagejudge());
            await task;
            btn2.Text = "停止ボタン";
            btn1.Enabled = true;
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
