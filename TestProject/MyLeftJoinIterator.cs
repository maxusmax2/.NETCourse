using System.Collections;

namespace TestProject;
public class MyLeftJoinIterator<T1, T2, TJoin, TReturn> : IEnumerable<TReturn>, IEnumerator<TReturn>
{
    private readonly IEnumerable<T1> _seqOne;
    private readonly IEnumerable<T2> _seqTwo;
    private readonly Func<T1, TJoin> _f1Join;
    private readonly Func<T2, TJoin> _f2Join;
    private readonly Func<T1, T2?, TJoin, TReturn> _returnVal;
    private IEnumerator<T1> _seqEnumeratorOne;
    private IEnumerator<T2> _seqEnumeratorTwo;
    private bool _findJoin;
    public MyLeftJoinIterator(IEnumerable<T1> t1, IEnumerable<T2> t2, Func<T1, TJoin> f1Join, Func<T2, TJoin> f2Join, Func<T1, T2?, TJoin, TReturn> returnVal)
    {
        _seqOne = t1;
        _seqTwo = t2;
        _seqEnumeratorOne = _seqOne.GetEnumerator();
        _seqEnumeratorOne.MoveNext();
        _seqEnumeratorTwo = _seqTwo.GetEnumerator();
        _f1Join = f1Join;
        _f2Join = f2Join;
        _returnVal = returnVal;
    }

    public IEnumerator<TReturn> GetEnumerator()
    {
        Reset();
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();


    public TReturn Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _seqEnumeratorOne.Dispose();
        _seqEnumeratorTwo.Dispose();
    }

    public bool MoveNext()
    {
        do
        {
            if (!_seqEnumeratorTwo.MoveNext())
            {

                _seqEnumeratorTwo.Reset();

                if (_findJoin)
                {
                    _findJoin = false;

                    if (!_seqEnumeratorOne.MoveNext())
                        return false;

                    continue;
                }
                else
                {
                    Current = _returnVal(_seqEnumeratorOne.Current, default, _f1Join(_seqEnumeratorOne.Current));

                    if (!_seqEnumeratorOne.MoveNext())
                        return false;
                    return true;
                }
            }
            else
            {
                if (_f1Join(_seqEnumeratorOne.Current).Equals(_f2Join(_seqEnumeratorTwo.Current))) 
                {
                    Current = _returnVal(_seqEnumeratorOne.Current, _seqEnumeratorTwo.Current, _f1Join(_seqEnumeratorOne.Current));
                    _findJoin = true;
                    return true;
                }
            }
        }
        while (true);

    }

    public void Reset()
    {
        _seqEnumeratorOne = _seqOne.GetEnumerator();
        _seqEnumeratorOne.MoveNext();
        _seqEnumeratorTwo= _seqTwo.GetEnumerator();
    }

}
