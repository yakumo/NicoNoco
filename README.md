# NicoNoco

small Twitter client with Xamarin

## Xamarin.Forms + CoreTweet

NicoNoco�́A�ƂĂ��ȈՓI��Twitter�N���C�A���g�ł��B  
Xamarin.Forms�̃T���v���Ƃ��Ă������������B  

## �o���鎖

* �X�g���[��API���g���ă��A���^�C����Tweet��\���ł��܂�
* Tweet���������ނ��Ƃ��ł��܂�

���݁A���ꂭ�炢�����ł��܂���B

## �g����

### �X�g���[���J�n

�A�v�����N�����APIN�F�؂��ς܂���ƁA��ʏ㕔�� `Stream` ���j���[���o�Ă��܂��B  
�������x�N���b�N����ƁA�X�g���[��API���J�n���A��M����Tweet���\������܂��B  
�ēx�������ƂŒ�~���܂��B  

### Tweet��������

��ʏ㕔�� `Tweet` ���j���[���N���b�N����Ɠ��͗�������ATweet����͂��邱�Ƃ��ł��܂��B

# �r���h�ɂ���

## ���̂܂܂ł̓r���h�ł��܂���

Clone���������ł̓t�@�C��������Ȃ����߁A�r���h�ł��܂���B  
NicoNocoApp.Common/SystemSettings.cs���ȉ��̓��e�ō쐬���Ă��������B  

```
namespace NicoNocoApp.Common
{
    class SystemSettings
    {
        public const string ConsumerKey = "xxx";
        public const string ConsumerSecret = "xxx";
    }
}
```

xxx�̕����́ATwitter�ɊJ���ғo�^�E�A�v���o�^�����Ď擾�ł��� `Consumer Key` �� `Consumer Secret` ��ݒ肵�Ă��������B

# ����

## ����̕��j

�ׁX�Ɖ��ǂ��Ă����܂��B  
�C����������X�g�A�ɓo�^���邩������܂���B  

## TODO

- [ ] �X���[�v�E�w�ʂ։��������Tweet�擾���~����
- [ ] �X�g���[�����j���[�ŊJ�n�����Ƃ��ɁA�Ԋu�������Ă����炻�̊Ԃ�Tweet���擾����
- [ ] �摜���e
- [ ] �^�u�@�\��DM��Mension���\������悤�ɕύX
- [ ] DM��Memsion��Notification�\��
