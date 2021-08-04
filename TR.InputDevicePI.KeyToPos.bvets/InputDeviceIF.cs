using Mackoy.Bvets;

using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace TR.InputDevicePI.KeyToPos.bvets
{
	public class InputDeviceIF : IInputDevice, IMessageFilter
	{
		/// <summary>ハンドルの操作命令を送る</summary>
		public event InputEventHandler? LeverMoved;

		/// <summary>最後に入力されたrangesを記録</summary>
		int[][] last_ranges = new int[0][];

		/// <summary>力行ハンドルの操作命令文字とMoveToの対応</summary>
		Dictionary<char, int> PowerPosDic { get; } = new()
		{
			['e'] = 0,
			['r'] = 1,
			['t'] = 2,
			['y'] = 3,
			['u'] = 4,
			['i'] = 5,
		};

		/// <summary>制動ハンドルの操作命令文字とMoveToの対応</summary>
		Dictionary<char, int> BrakePosDic { get; } = new()
		{
			['w'] = 0,
			['s'] = 1,
			['d'] = 2,
			['f'] = 3,
			['g'] = 4,
			['h'] = 5,
			['j'] = 6,
			['k'] = 7,
			['l'] = 8,
			[';'] = 9,
		};

		static class Axis
		{
			internal const int FuncKey = -1;
			internal const int ATSKey = -2;
			internal const int Reverser = 0;
			internal const int Power = 1;
			internal const int Brake = 2;
			internal const int SHandle = 3;
			internal const int Positive = 1;
			internal const int Negative = 0;
		}

		public void Configure(IWin32Window owner)
		{
			StringBuilder sb = new StringBuilder();

			var asmName = Assembly.GetExecutingAssembly().GetName();

			//アセンブリ名を出力
			sb.AppendLine(asmName.Name);

			//バージョンを出力
			sb.Append("Version : ").AppendLine(asmName.Version?.ToString());

			//可動範囲情報を出力
			sb.AppendLine().AppendLine("Ranges");
			if (last_ranges.Length > 0)
			{
				sb.AppendFormat("Reverser\t: {0} ~ {1}", last_ranges[Axis.Reverser][Axis.Negative], last_ranges[Axis.Reverser][Axis.Positive]).AppendLine();
				sb.AppendFormat("   Brake\t: {0} ~ {1}", last_ranges[Axis.Brake][Axis.Negative], last_ranges[Axis.Brake][Axis.Positive]).AppendLine();
				sb.AppendFormat("   Power\t: {0} ~ {1}", last_ranges[Axis.Power][Axis.Negative], last_ranges[Axis.Power][Axis.Positive]).AppendLine();
				sb.AppendFormat(" SHandle\t: {0} ~ {1}", last_ranges[Axis.SHandle][Axis.Negative], last_ranges[Axis.SHandle][Axis.Positive]).AppendLine();
			}

			MessageBox.Show(owner, sb.ToString());
		}

		public void Dispose() => Application.RemoveMessageFilter(this);

		public void Load(string settingsPath)
		{
#if DEBUG
			if (!System.Diagnostics.Debugger.IsAttached)
				System.Diagnostics.Debugger.Launch();
#endif

			//設定ファイルはdllと同じディレクトリに配置させ, 見つからなければsettingsPathの方を確認する
			//とりあえず未実装
			//どうせなら同時押しにも対応できるような形にしたい

			//メッセージフィルタを登録
			//ref : https://csharp.keicode.com/basic/message-filter.php
			Application.AddMessageFilter(this);
		}

		public void SetAxisRanges(int[][] ranges) => last_ranges = ranges;

		const int WM_CHAR = 0x0102;

		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg == WM_CHAR) //文字入力があった際に呼ばれる
			{
				char c = (char)m.WParam; //入力された文字を取得
				System.Diagnostics.Debug.WriteLine("Key : " + c);
				if (PowerPosDic.TryGetValue(c, out var vp))
					LeverMoved?.Invoke(this, new(Axis.Power, vp)); //力行操作命令

				if (BrakePosDic.TryGetValue(c, out var vb))
					LeverMoved?.Invoke(this, new(Axis.Brake, vb)); //力行がセットされても, 制動に対応があるなら上書きする
			}

			return false;
		}


		#region Unused events / methods

		public void Tick() { }

#pragma warning disable CS0067

		/// <summary>キー押下命令を送る</summary>
		public event InputEventHandler? KeyDown;

		/// <summary>キーから離す命令を送る</summary>
		public event InputEventHandler? KeyUp;

#pragma warning restore CS0067
		#endregion
	}
}
