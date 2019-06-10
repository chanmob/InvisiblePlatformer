using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberCounting : Singleton<NumberCounting>
{
    public void StartCount(Text _text, float _min, float _max, float _time, float _delayTime = 0, bool _isFloat = false)
    {
        StartCoroutine(CountingCoroutine(_text, _min, _max, _time, _delayTime, _isFloat));
    }

    private IEnumerator CountingCoroutine(Text _text, float _min, float _max, float _time, float _delayTime, bool _isFloat)
    {
        if(_delayTime > 0)
            yield return new WaitForSeconds(_delayTime);

        float offset = (_max - _min) / _time;

        while(_min < _max)
        {
            _min += offset * Time.deltaTime;
            if (_isFloat)
                _text.text = _min.ToString("0.00");
            else
                _text.text = ((int)_min).ToString();
            yield return null;
        }

        _min = _max;

        if(_isFloat)
            _text.text = _min.ToString("0.00");
        else
            _text.text = ((int)_min).ToString();
    }
}
