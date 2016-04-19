# NicoNoco

small Twitter client with Xamarin

## Xamarin.Forms + CoreTweet

NicoNocoは、とても簡易的なTwitterクライアントです。  
Xamarin.Formsのサンプルとしてご覧ください。  

## 出来る事

* ストリームAPIを使ってリアルタイムにTweetを表示できます
* Tweetを書き込むことができます

現在、これくらいしかできません。

## 使い方

### ストリーム開始

アプリを起動し、PIN認証を済ませると、画面上部に `Stream` メニューが出ています。  
これを一度クリックすると、ストリームAPIを開始し、受信したTweetが表示されます。  
再度押すことで停止します。  

### Tweet書き込み

画面上部の `Tweet` メニューをクリックすると入力欄が現れ、Tweetを入力することができます。

# ビルドについて

## このままではビルドできません

Cloneしただけではファイルが足りないため、ビルドできません。  
NicoNocoApp.Common/SystemSettings.csを以下の内容で作成してください。  

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

xxxの部分は、Twitterに開発者登録・アプリ登録をして取得できる `Consumer Key` と `Consumer Secret` を設定してください。

# 今後

## 今後の方針

細々と改良していきます。  
気が向いたらストアに登録するかもしれません。  

## TODO

- [ ] スリープ・背面へ回った時にTweet取得を停止する
- [ ] ストリームメニューで開始したときに、間隔があいていたらその間のTweetを取得する
- [ ] 画像投稿
- [ ] タブ機能でDMやMensionも表示するように変更
- [ ] DMやMemsionのNotification表示
