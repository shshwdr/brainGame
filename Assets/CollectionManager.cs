using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using System.Linq;
using System.Collections;

public class CollectionManager : Singleton<CollectionManager>
{

	//References
	[Header("UI references")]
	[SerializeField] TMP_Text coinUIText;
	[SerializeField] GameObject animatedCoinPrefab;

	[Space]
	[Header("Available coins : (coins to pool)")]
	[SerializeField] int maxCoins;
	Queue<GameObject> coinsQueue = new Queue<GameObject>();


	[Space]
	[Header("Animation settings")]
	[SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
	[SerializeField]  float maxAnimDuration;

	[SerializeField] Ease easeType;
	[SerializeField] float spread;



	private int _c = 0;

	public int Coins
	{
		get { return _c; }
		set
		{
			_c = value;
			//update UI text whenever "Coins" variable is changed
			//coinUIText.text = Coins.ToString();
		}
	}

	void Awake()
	{


		//prepare pool
		PrepareCoins();
	}

	void PrepareCoins()
	{
		GameObject coin;
		for (int i = 0; i < maxCoins; i++)
		{
			coin = Instantiate(animatedCoinPrefab);
			coin.transform.parent = transform;
			coin.SetActive(false);
			coinsQueue.Enqueue(coin);
		}
	}

	void MoveSource(Vector3 start, Vector3 end, Sprite sprite, int amount, Action action)
	{
		for (int i = 0; i < amount; i++)
		{
			//check if there's coins in the pool
			if (coinsQueue.Count > 0)
			{
				//extract a coin from the pool
				GameObject coin = coinsQueue.Dequeue();
				coin.GetComponent<SpriteRenderer>().sprite = sprite;
				coin.SetActive(true);

				//move coin to the collected coin pos
				coin.transform.position = start + new Vector3(Random.Range(-spread, spread), 0f, 0f);




				//animate coin to target position
				float duration = Random.Range(minAnimDuration, maxAnimDuration);
				coin.transform.DOMove(end, duration)
				.SetEase(easeType)
				.OnComplete(() =>
				{
					//executes whenever coin reach target position
					coin.SetActive(false);
					coinsQueue.Enqueue(coin);
					action();
				});
			}
		}
	}

	//public void RemoveCoins(Vector3 removedCoinPosition, Dictionary<PlantProperty, int> resource, bool onlyForNonIncreasingResrouce = false)
	//{
	//	foreach (var pair in resource)
	//	{
	//		if (onlyForNonIncreasingResrouce && PlantsManager.Instance.isIncreasingResource(pair.Key))
	//		{
	//			continue;
	//		}
	//		var amount = pair.Value;
	//		//get target position
	//		var target = HUD.Instance.hudByProperty[pair.Key].GetComponent<OneStatHud>().image.transform;
	//		Vector3 screenPoint = target.position + new Vector3(0, 0, 5);  //the "+ new Vector3(0,0,5)" ensures that the object is so close to the camera you dont see it

	//		//find out where this is in world space
	//		Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);




	//		MoveSource(worldPos, removedCoinPosition, pair.Key, pair.Value, () =>
	//		{
	//			PlantsManager.Instance.currentResource[pair.Key] -= 1;
	//			BeeManager.Instance.updateGenerateTime();
	//			PestManager.Instance.updateGenerateTime();
	//		});
	//	}
	//}

	public void AddCoins(Vector3 collectedCoinPosition, Dictionary<string, float> resource, bool isIncreasingResource = true)
	{
		foreach (var pair in resource)
		{
			var amount = pair.Value;
			var target = AttributeManager.Instance.attributeCellTrans[pair.Key];
			Vector3 screenPoint = target.position + new Vector3(0, 0, 5);  //the "+ new Vector3(0,0,5)" ensures that the object is so close to the camera you dont see it

			//find out where this is in world space
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);


			MoveSource(collectedCoinPosition, worldPos, AttributeManager.Instance.attributeDict[ pair.Key].icon,(int) pair.Value, () =>
			{
				AttributeManager.Instance.addAttribute(pair.Key, 1);
				//PlantsManager.Instance.currentResource[pair.Key] += 1;
				//BeeManager.Instance.updateGenerateTime();
				//PestManager.Instance.updateGenerateTime();
				//TutorialManager.Instance.collectResource(pair.Key);
			});
		}

	}
    private void Start()
    {

		//MoveSource(new Vector3(0, 0, 0), new Vector3(1,1,0), AttributeManager.Instance.attributeDict.Values.ToList()[0].icon, 3, () =>
		//{
		//	//AttributeManager.Instance.addAttribute(pair.Key, 1);
		//	//PlantsManager.Instance.currentResource[pair.Key] += 1;
		//	//BeeManager.Instance.updateGenerateTime();
		//	//PestManager.Instance.updateGenerateTime();
		//	//TutorialManager.Instance.collectResource(pair.Key);
		//});
		//StartCoroutine(test());
	}

	IEnumerator  test()
    {
		yield return new WaitForSeconds(0.1f);

		AddCoins(new Vector3(0, 0, 0), new Dictionary<string, float>() { { "财富", 5 } });
	}

}