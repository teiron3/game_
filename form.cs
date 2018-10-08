using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

///<summary>form �̌`��ƃC�x���g���L��</summary>
partial class form : Form{

    ///<summary>��~�t���O</summary>
    bool stop_flg = false;
    
    ///<summary>csv�t�@�C���̃f�[�^���i�[����</summary>
    ///<remarks>Dictionary �N���X��錾</remarks>
    public Dictionary<string, pic_data_class> p_class = new Dictionary<string, pic_data_class>();

    ///<summary>csv�t�@�C�����̐ݒ�</summary>
    public string csv_file{get{return "csv_file.csv";}}

    ///<summary>�_���[�W����p�t���O</summary>
    int damageRed = 0,damageOrange = 0;

    ///<summary>��������t���O</summary>
    ///<remarks>true �̂Ƃ��ɓ���</remarks>
    bool dockflg = false;

    ///<summary>�퓬�C�����p�����ǂ����̃t���O</summary>
    ///<remarks>true �Ōp���I��</remarks>
    bool continueflg = false;

    ///<summary>�L���t���p�̊͑����ϐ�</summary>
    ///<remarks>�����l 1</remarks>
    int gkantai = 1;

    ///<summary>�L���t���p���񐔕ϐ�</summary>
    ///<remarks>�����l 3</remarks>
    int garoundcnt = 3;

    //�v�f�̃N���X�錾�ƃC���X�^���X��
    Button btn1 = new Button();
    Button btn2 = new Button();
    Button btn3 = new Button();
    RadioButton rbtn1 = new RadioButton();
    RadioButton rbtn2 = new RadioButton();
    RadioButton rbtn3 = new RadioButton();
    RadioButton rbtn4 = new RadioButton();

    Label lbl1 = new Label();
    Label lbl2 = new Label();
    Label lbl3 = new Label();
    Label lbl4 = new Label();
    Label lbl5 = new Label();
    Label lbl6 = new Label();
    Label lbl7 = new Label();
    Label lbl8 = new Label();

    ///<summary>�R���g���N�^(�`��̐ݒ�)</summary>
    public form(){
        // form�̑傫���ƃ^�C�g����ݒ�
        this.Text = "game manupirate";
        this.Size = new Size(1200, 200);
        this.StartPosition = FormStartPosition.Manual;
        this.Location = new Point(0, 730);

        //�e��v�f��ݒ�

        //�{�^��
        btn1.Parent = this;
        btn1.Location = new Point(15, 90);
        btn1.Size = new Size(90, 40);
        btn1.Text = "�X�^�[�g";
        btn1.Click += new EventHandler(btn1_action_start);

        btn2.Parent = this;
        btn2.Location = new Point(120, 90);
        btn2.Size = new Size(90, 40);
        btn2.Text = "��~�{�^��";
        btn2.Click += new EventHandler(btn2_end_key);

        btn3.Parent = this;
        btn3.Location = new Point(240, 90);
        btn3.Size = new Size(90, 40);
        btn3.Text = "�E�B���h�E�ʒu�C��";
        btn3.Click += new EventHandler(btn3_window);

        //���W�I�{�^��
        rbtn1.Parent = this;
        rbtn1.Location = new Point(15, 20);
        rbtn1.Text = "�����̂�";

        rbtn2.Parent = this;
        rbtn2.Location = new Point(140, 20);
        rbtn2.Text = "test";

        rbtn3.Parent = this;
        rbtn3.Location = new Point(15, 55);
        rbtn3.Text = "�L���t������(�����t��)";

        rbtn4.Parent = this;
        rbtn4.Location = new Point(140, 55);
        rbtn4.Text = "";

        //���x��(����p)
        lbl1.Parent = this;
        lbl1.Location = new Point(265, 20);
        lbl1.Size = new Size(200, 30);
        lbl1.Font = new Font(lbl1.Font.FontFamily, 20);
        lbl1.Text = "�L���t���͑���";

        lbl2.Parent = this;
        lbl2.Location = new Point(475, 20);
        lbl2.Size = new Size(30, 30);
        lbl2.Text = "�{";
        lbl2.Font = new Font(lbl2.Font.FontFamily, 20);
        lbl2.BackColor = Color.Red;
        lbl2.Click += (sender, e) =>{
            gkantai = (gkantai >= 5) ? 5 : gkantai + 1;
            lbl4.Text = gkantai.ToString();
        } ;

        lbl3.Parent = this;
        lbl3.Location = new Point(575, 20);
        lbl3.Size = new Size(30, 30);
        lbl3.Font = new Font(lbl3.Font.FontFamily, 20);
        lbl3.BackColor = Color.Green;
        lbl3.Text = "�|";
        lbl3.Click += (sender, e) =>{
            gkantai = (gkantai <= 1) ? 1 : gkantai - 1;
            lbl4.Text = gkantai.ToString();
        } ;

        lbl4.Parent = this;
        lbl4.Location = new Point(525, 20);
        lbl4.Size = new Size(30, 30);
        lbl4.Font = new Font(lbl4.Font.FontFamily, 20);
        lbl4.Text = gkantai.ToString();

        lbl5.Parent = this;
        lbl5.Location = new Point(265, 55);
        lbl5.Size = new Size(200, 30);
        lbl5.Font = new Font(lbl5.Font.FontFamily, 20);
        lbl5.Text = "�L���t������";

        lbl6.Parent = this;
        lbl6.Location = new Point(475, 55);
        lbl6.Size = new Size(30, 30);
        lbl6.Font = new Font(lbl6.Font.FontFamily, 20);
        lbl6.BackColor = Color.Red;
        lbl6.Text = "�{";
        lbl6.Click += (sender, e) =>{
            garoundcnt = (garoundcnt >= 3) ? 3 : garoundcnt + 1;
            lbl8.Text = garoundcnt.ToString();
        } ;

        lbl7.Parent = this;
        lbl7.Location = new Point(575, 55);
        lbl7.Size = new Size(30, 30);
        lbl7.Font = new Font(lbl7.Font.FontFamily, 20);
        lbl7.BackColor = Color.Green;
        lbl7.Text = "�|";
        lbl7.Click += (sender, e) =>{
            garoundcnt = (garoundcnt <= 1) ? 1 : garoundcnt - 1;
            lbl8.Text = garoundcnt.ToString();
        } ;

        lbl8.Parent = this;
        lbl8.Location = new Point(525, 55);
        lbl8.Size = new Size(30, 30);
        lbl8.Font = new Font(lbl8.Font.FontFamily, 20);
        lbl8.Text = garoundcnt.ToString();
    }

    //�C�x���g�̐ݒ�
    //btn1 �̃C�x���g(action start)
    private async void btn1_action_start(object sender, EventArgs e){
        btn1.Enabled = false;
        if(rbtn1.Checked == true){
            await Task.Run(()=> expendjitiononly());
            btn2.Text = "��~�{�^��";
            btn1.Enabled = true;
            stop_flg = false;
            return;
        }

        if(rbtn2.Checked == true){
            Task task = Task.Run(()=> plantest());
            await task;
            btn2.Text = "��~�{�^��";
            btn1.Enabled = true;
            stop_flg = false;
            return;
        }
        if(rbtn3.Checked == true){
            Task task = Task.Run(()=> around1_1());
            await task;
            btn2.Text = "��~�{�^��";
            btn1.Enabled = true;
            stop_flg = false;
            return;
        }
        /*
        if(rbtn4.Checked == true){
            Task.Run(()=> testmeso(this.x));
            return;
        }
         */

        //���I�����̏I������
        btn1.Enabled = true;
        stop_flg = false;
        return;
        
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
