using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public Transform charaTrans;
    public List<Transform> beans;

	public Transform closestBean
	{
		get
		{
			Transform transform = beans[0];
            float minDistance = Vector3.Distance(charaTrans.position, beans[0].position);
			foreach (var item in beans)
			{
				float distance = Vector3.Distance(charaTrans.position, item.position);
                if (distance < minDistance)
				{
					minDistance = distance;
					transform = item;
				}
			} 
			return transform;
		} private set { } }

    private void Awake()
    {
        Instance = this;
    }

}