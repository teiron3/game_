using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

partial class form{
    // csv�t�@�C���̃f�[�^���i�[���� Dictionary �N���X��錾
    public Dictionary<string, pic_data_class> p_class = new Dictionary<string, pic_data_class>();

    // csv�t�@�C�����̐ݒ�
    public string csv_file{get{return "csv_file.csv";}}

    //csv�t�@�C����ǂݍ���Ńf�[�^�^�N���X�ɓ���郁�\�b�h
    //����ǂݍ��݂ł��Ȃ������Ƃ��� false ��Ԃ� 
    public bool read_csv(){

        //����ɓǂݍ��݂ł������ǂ����𔻒�p
        bool flg = true;

        string file_path = csv_file;
        if(!System.IO.File.Exists(file_path)) {
            logwrite_msgbox("error:�ݒ�p��csv�t�@�C��������܂���");
            stop_flg = true;
            return false;
        }

        System.IO.StreamReader text_strm = new System.IO.StreamReader(file_path, System.Text.Encoding.GetEncoding("shift_jis"));
        while(text_strm.Peek() >= 0){
            string s = text_strm.ReadLine();
            //�R�����g�A�E�g # �̏���
            if(s.StartsWith("#"))continue;
            string[] test_str;
            test_str = s.Split(',');
            
            p_class.Add(test_str[0], new pic_data_class());
            var tmp = p_class[test_str[0]];
            
            tmp.Name = test_str[0];
            if(test_str.Length >= 2) tmp.Set_Necessity = test_str[1];
            if(test_str.Length >= 3) tmp.X = int.Parse(test_str[2]);
            if(test_str.Length >= 4) tmp.Y = int.Parse(test_str[3]);
            if(test_str.Length >= 5) tmp.Width = int.Parse(test_str[4]);
            if(test_str.Length >= 6) tmp.Height = int.Parse(test_str[5]);
            if(test_str.Length >= 7) tmp.Pic_X = int.Parse(test_str[6]);
            if(test_str.Length >= 8) tmp.Pic_Y = int.Parse(test_str[7]);
            if(test_str.Length >= 9) tmp.Pic_Width = int.Parse(test_str[8]);
            if(test_str.Length >= 10) tmp.Pic_Height = int.Parse(test_str[9]);
            if(test_str.Length >= 11) tmp.Pic_CreateDate = test_str[10];
            
            if(tmp.Necessity == true)
            {
                if(System.IO.File.Exists(tmp.Address))
                    tmp.Pic_data = new Bitmap(tmp.Address);
                else
                    logwrite("error:" + tmp.Name + "��bmp�t�@�C��������܂���");
                    flg = false;
                    stop_flg = true;
            }
        }

        text_strm.Close();
        return flg;
    }

}

//�ݒ�csv�t�@�C���̏����i�[����f�[�^�^�N���X
class pic_data_class{
    bool need;

    public string Name{get;set;}
    public bool Necessity{get{return need;}set{need = value;}}
    public string Set_Necessity{get{return "";} set{ if(value == "True") need = true;else need = false;}}
    public int X{get;set;}
    public int Y{get;set;}
    public int Width{get;set;}
    public int Height{get;set;}
    public int Pic_X{get;set;}
    public int Pic_Y{get;set;}
    public int Pic_Width{get;set;}
    public int Pic_Height{get;set;}
    public string Pic_CreateDate{get;set;}
    public Bitmap Pic_data{get;set;}
    public string Address{get{return @".\pic_folder\" + this.Name + ".bmp";}}

}

