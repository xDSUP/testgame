using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Enemy : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;


    //настройки врага
    public float speed = 3; // скорость передвижения 
    private int _anxiety = 0; //начальное кол-во страха 
    private bool _isPanic = false; //режим паники

    //настройки перемещения
    public GameObject[] WayPoints; // получаем достпуные для перемещения точки
    public float waitingTime = 5;//сколько находится в комнате 
    public ArrayList investigatedWayPoints = new ArrayList(); // храним индексы исследованных комнат
    public bool inWay = false;
    //public bool isExplore = false; //
    private int _currentWayPoint = 0; // текущ комната
    private int _nextWayPoint = 1; // след комната , изначально холл
    private float _lastWayPointswitchTime;// время последнего перехода
   
    

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _lastWayPointswitchTime = Time.time;
    }

    //добавляем тревогу
    public void addAnxiety(int Anxiety)
    {
        _anxiety += Anxiety;
    }

    //проверка состояния паники
    public bool isPanic()
    {
        if (_anxiety < 100)
            return false;
        return true;
    }

    //Нахождение неисследованной комнаты комнаты
    private int getNewWayPoint()
    {
        int indWayPoint = _currentWayPoint;
        //заменить
        
        if (investigatedWayPoints.IndexOf(indWayPoint) != -1)
        {
            return indWayPoint++;
        }
        //TODO: Нахождение неисследованной комнаты комнаты
        return indWayPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPanic() == false && Time.time >= _lastWayPointswitchTime + waitingTime) // если не в панике , тогда обычный режим
        {
            Vector3 startPosition = WayPoints[_currentWayPoint].transform.position;
            Vector3 endPosition = WayPoints[_nextWayPoint].transform.position;

            float pathLength = Vector3.Distance(startPosition, endPosition);
            float totalTimeForPath = pathLength / speed;
            Debug.Log(gameObject.transform.position.Equals(endPosition));
            float currentTimeOnPath = Time.time - _lastWayPointswitchTime;
            
            gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
            Debug.Log(Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath));

            if (gameObject.transform.position.Equals(endPosition))
            {
                if (_currentWayPoint < WayPoints.Length - 2)//проверка на нахождение склепа
                {
                    // 3.a 
                    investigatedWayPoints.Add(_currentWayPoint);
                    _currentWayPoint = _nextWayPoint;
                    _nextWayPoint = getNewWayPoint();
                    _lastWayPointswitchTime = Time.time;
                    Debug.Log("приехал");
                    // TODO: поворачиваться в направлении движения
                }
                else
                {
                    // Destroy(gameObject);
                    //если парни нашли то....нам пздц
                }
            }
        }
        else
        {
            // TODO: пытаемся убежать из замка

        }
    }
}
