# ik勉強用

## ikとは
3dモデル、スケルトン構造を制御する方法の1つ。一般的では親の位置->子の位置という計算順番になるが、IKは子の位置->親の位置になります

## メリット
3dモーションの細かい調整ができる。モーションをより自然にする

## 応用例
手すりを掴む、手をつなぐ、ものを掴む、肩を叩くなどのシーンの場合
手首の位置を決めて->前腕、上腕の位置を算出させば、動きは自然に見えて、調整もやりやすい

<img width="382" alt="スクリーンショット 2023-04-04 17 33 39" src="https://user-images.githubusercontent.com/59904787/229735325-ec26cf40-e2e6-41cb-9aa2-2b5cdca3bb5a.png">

## 使用するunityのパッケージ
animation rigging -> package managerからダウンロードできる

## 自分の制作内容
下記動画↓
idle、runの2つモーションはあって、その2つモーションは銃を掴む際に、ikでモーションを上書きし、両手の手首で銃の取手を掴むようなモーションになった

https://user-images.githubusercontent.com/59904787/229743718-a67d6c88-a2dc-47dc-97d4-42fe9ba141aa.mov


## 操作
WASDで移動、画面ドラックで視点変更、Eキーで武器拾う

