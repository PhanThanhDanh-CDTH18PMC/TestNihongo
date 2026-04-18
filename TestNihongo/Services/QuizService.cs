using NihongoVocabTrainer.Models;

namespace NihongoVocabTrainer.Services
{
	/// <summary>
	/// クイズ作成処理を行います。
	/// </summary>
	public class QuizService
	{
		/// <summary>
		/// 単語一覧からランダムに1件取得します。
		/// </summary>
		/// <param name="vocabularies">単語一覧</param>
		/// <returns>単語</returns>
		public Vocabulary? GetRandomVocabulary(List<Vocabulary> vocabularies)
		{
			#region ランダム単語取得

			if (vocabularies == null || vocabularies.Count == 0)
			{
				return null;
			}

			var random = new Random();
			int index = random.Next(vocabularies.Count);

			return vocabularies[index];

			#endregion
		}
	}
}