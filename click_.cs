// �}�E�X�J�[�\�����A�w�肵���ʒu�ֈړ�������A�N���b�N�����肷��

// "user32.dll" �� SendInput() ���g���A�}�E�X�C�x���g�𐶐�����̂ŁA
// ���̃A�v���P�[�V�����̃E�B���h�E�E�I�u�W�F�N�g���N���b�N���邱�Ƃ��\
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;  // for DllImport, Marshal

//DLL�̐ݒ�
partial class mouse_class{
    [DllImport("user32.dll")]
    extern static uint SendInput(
        uint nInputs,   // INPUT �\���̂̐�(�C�x���g��)
        INPUT[] pInputs,   // INPUT �\����
        int cbSize     // INPUT �\���̂̃T�C�Y
    );

    [StructLayout(LayoutKind.Sequential)]  // �A���}�l�[�W DLL �Ή��p struct �L�q�錾
    struct INPUT
    {
        public int type;  // 0 = INPUT_MOUSE(�f�t�H���g), 1 = INPUT_KEYBOARD
        public MOUSEINPUT mi;
        // Note: struct �̏ꍇ�A�f�t�H���g(�p�����[�^�Ȃ���)�R���X�g���N�^�́A
        //       ���ꑤ�Œ�`�ς݂ŁA�t�B�[���h�� 0 �ɏ���������B
    }

    [StructLayout(LayoutKind.Sequential)]  // �A���}�l�[�W DLL �Ή��p struct �L�q�錾
    struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;  // amount of wheel movement
        public int dwFlags;
        public int time;  // time stamp for the event
        public IntPtr dwExtraInfo;
        // Note: struct �̏ꍇ�A�f�t�H���g(�p�����[�^�Ȃ���)�R���X�g���N�^�́A
        //       ���ꑤ�Œ�`�ς݂ŁA�t�B�[���h�� 0 �ɏ���������B
    }


    // dwFlags
    const int MOUSEEVENTF_MOVED = 0x0001;
    const int MOUSEEVENTF_LEFTDOWN = 0x0002;  // ���{�^�� Down
    const int MOUSEEVENTF_LEFTUP = 0x0004;  // ���{�^�� Up
    const int MOUSEEVENTF_RIGHTDOWN = 0x0008;  // �E�{�^�� Down
    const int MOUSEEVENTF_RIGHTUP = 0x0010;  // �E�{�^�� Up
    const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;  // ���{�^�� Down
    const int MOUSEEVENTF_MIDDLEUP = 0x0040;  // ���{�^�� Up
    const int MOUSEEVENTF_WHEEL = 0x0080;
    const int MOUSEEVENTF_XDOWN = 0x0100;
    const int MOUSEEVENTF_XUP = 0x0200;
    const int MOUSEEVENTF_ABSOLUTE = 0x8000;

    const int screen_length = 0x10000;  // for MOUSEEVENTF_ABSOLUTE (���̒l�͌Œ�)
}


//���\�b�h�Ȃ�
partial class mouse_class{
    //�����_���������p
    int rnd(int rr){
        Random r = new System.Random();
        return r.Next(rr);
    }

    //���� pic_class�I�u�W�F�N�g ����C�ӂ͈̔͂��N���b�N����
    public void before_move_and_click(pic_data_class obj){
        Cursor.Position = new Point(obj.X + rnd(obj.Width), obj.Y + rnd(obj.Height));
        click_L();
        System.Threading.Thread.Sleep(rnd(1000) + 1000);
    }

    //���� pic_class�I�u�W�F�N�g ����C�ӂ͈̔͂��N���b�N����
    public void later_move_and_click(pic_data_class obj){
        Cursor.Position = new Point(obj.X + rnd(obj.Width), obj.Y + rnd(obj.Height));
        System.Threading.Thread.Sleep(rnd(1000) + 1000);
        click_L();
    }

    //���� pic_class�I�u�W�F�N�g ����C�ӂ͈̔͂Ƀ}�E�X���ړ�������
    public void move_cursor(pic_data_class obj){
        Cursor.Position = new Point(obj.X + rnd(obj.Width), obj.Y + rnd(obj.Height));
    }

    //���N���b�N����
    public void click_L(){
        /*** �}�E�X�J�[�\���̈ړ��ƁA�h���b�O����̗� ***/
        // �h���b�O����̏��� (struct �z��̐錾)
        INPUT[] input = new INPUT[2];  // �v3�C�x���g���i�[

        // �h���b�O����̏��� (��1�C�x���g�̒�` = ���{�^�� Down)
        input[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

        // �h���b�O����̏��� (��2�C�x���g�̒�` = ��΍��W�ֈړ�)
        //input[1].mi.dx = screen_length / 2;  // X ���W = ��� 1/2 (����)
        //input[1].mi.dy = screen_length / 2;  // Y ���W = ��� 1/2 (����)
        //input[1].mi.dwFlags = MOUSEEVENTF_MOVED | MOUSEEVENTF_ABSOLUTE;

        // �h���b�O����̏��� (��3�C�x���g�̒�` = ���{�^�� Up)
        input[1].mi.dwFlags = MOUSEEVENTF_LEFTUP;

        // �h���b�O����̎��s (�v3�C�x���g�̈ꊇ����)
        SendInput(2, input, Marshal.SizeOf(input[0]));
    }
}

// Note: �}�E�X�J�[�\���̈ړ����@�́ACursor.Position �� SendInput() ��2�ʂ肠�邪�A
//       �h���b�O���쒆�́u�}�E�X�J�[�\���̈ړ��v�́A�r���Ŋ��荞�݂�����Ȃ��悤
//       SendInput() �ōs���������S�ł���B

// Note: MOUSEEVENTF_ABSOLUTE �ł̍��W�w��́A����ȍ��W�P�ʌn�Ȃ̂Œ��ӂ���B
//       ��ʍ���̃R�[�i�[�� (0, 0)�A��ʉE���̃R�[�i�[�� (65535, 65535)�ł���B

// Note: No MOUSEEVENTF_ABSOLUTE �ł̍��W�w��́A���΍��W�n�ɂȂ邪�A�P�ʂ��K��
//       ���� 1px �ł͂Ȃ��̂Œ��ӂ���B
//       �e PC �Őݒ肳�ꂽ mouse speed �� acceleration level �Ɉˑ�����B

// Note: SendInput()�p�����[�^�̏ڍׂ́AMSDN�w MOUSEINPUT Structure �x���Q�Ƃ���B

