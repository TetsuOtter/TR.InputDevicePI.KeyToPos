# TR.InputDevicePI.KeyToPos

キー入力があった際に, そのキーに対応する位置にハンドルをセットする入力デバイスプラグインです.

Version 1.0.0時点では, 次の対応が実装されています.

|Key|Type|Pos|
|---|---|---|
|`;`|Brake|9|
|`l`|Brake|8|
|`k`|Brake|7|
|`j`|Brake|6|
|`h`|Brake|5|
|`g`|Brake|4|
|`f`|Brake|3|
|`d`|Brake|2|
|`s`|Brake|1|
|`w`|Brake|0|
|`e`|Power|0|
|`r`|Power|1|
|`t`|Power|2|
|`y`|Power|3|
|`u`|Power|4|
|`i`|Power|5|

## How to install

1. [TetsuOtter/TR.InputDevicePI.KeyToPos](https://github.com/TetsuOtter/TR.InputDevicePI.KeyToPos)のReleaseページに移動します
2. Latest ReleaseのAssetsから「TR.InputDevicePI.KeyToPos.zip」をダウンロードします
3. ダウンロードしたzipファイルを解凍します
4. 解凍先にある「TR.InputDevicePI.KeyToPos.bvets.dll」を, BVEの実行ファイルと同じディレクトリにある「Input Devices」フォルダにコピペします
5. BVEを起動し, BVEの画面内で右クリックして「設定」をクリックします
6. 設定ウィンドウで「入力デバイス」タブを開き, 表示されたリスト中の「KeyToPos」行左端にあるチェックボックスにチェックを入れてください.

## How to use

BVEのウィンドウにてキー入力を行うことで 対応するハンドル位置にセットされます.

## License

MITライセンスの下で, 自由に使用/改造/再配布等可能です.