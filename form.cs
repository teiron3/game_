using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

// form の形状とイベントを記載
partial class form : Form{

    //停止フラグ
    bool stop_flg = false;

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
        this.Width = 200;
        this.Height = 400;

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
        rbtn1.Text = "test";

        rbtn2.Parent = this;
        rbtn2.Location = new Point(15, 40);
        rbtn2.Text = "test2";

        rbtn3.Parent = this;
        rbtn3.Location = new Point(15, 60);
        rbtn3.Text = "test4";

        rbtn4.Parent = this;
        rbtn4.Location = new Point(15, 80);
        rbtn4.Text = "test5";
    }

    //イベントの設定
    //btn1 のイベント(action start)
    void btn1_action_start(object sender, EventArgs e){
        if(rbtn1.Checked == true){
            Task.Run(()=> supply((byte)15));
            return;
        }

        /*
        if(rbtn2.Checked == true){
            Task.Run(()=> testmeso(this.x));
            return;
        }
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
