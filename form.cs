using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

// form �̌`��ƃC�x���g���L��
partial class form : Form{

    //��~�t���O
    bool stop_flg = false;

    //�v�f�̃N���X�錾�ƃC���X�^���X��
    Button btn1 = new Button();
    Button btn2 = new Button();
    Button btn3 = new Button();
    RadioButton rbtn1 = new RadioButton();
    RadioButton rbtn2 = new RadioButton();
    RadioButton rbtn3 = new RadioButton();
    RadioButton rbtn4 = new RadioButton();

    //�R���g���N�^(�`��̐ݒ�)
    public form(){
        // form�̑傫���ƃ^�C�g����ݒ�
        this.Text = "game manupirate";
        this.Width = 200;
        this.Height = 400;

        //�e��v�f��ݒ�

        //�{�^��
        btn1.Parent = this;
        btn1.Location = new Point(50, 150);
        btn1.Text = "�X�^�[�g";
        btn1.Click += new EventHandler(btn1_action_start);

        btn2.Parent = this;
        btn2.Location = new Point(50, 190);
        btn2.Text = "��~�{�^��";
        btn2.Click += new EventHandler(btn2_end_key);

        btn3.Parent = this;
        btn3.Location = new Point(50, 230);
        btn3.Text = "�E�B���h�E�ʒu�C��";
        btn3.Click += new EventHandler(btn3_window);

        //���W�I�{�^��
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

    //�C�x���g�̐ݒ�
    //btn1 �̃C�x���g(action start)
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

    //btn2�̃C�x���g(action stop)
    void btn2_end_key(object sender, EventArgs e){
        btn2.Text = "��~������";
        this.stop_flg = true;
    }

    //�E�C���h�E�ʒu�̏C�����������{�^���C�x���g
    void btn3_window(object sender, EventArgs e){
        new handmade.operation_window();
    }
    
    
}
