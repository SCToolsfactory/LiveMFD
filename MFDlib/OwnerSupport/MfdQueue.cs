using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFDlib.OwnerSupport
{
  /// <summary>
  /// An Object to cycle MFDs for use - handles Visible Property
  ///  Start: for all MFDs Add(mfd)
  ///  Then either GetNext() or GetPrev()
  /// </summary>
  public class MfdCycler
  {
    private int m_currentIndex = -1; // the one in use

    private List<IUC_MFD> m_list = new List<IUC_MFD>( );

    /// <summary>
    /// Enumerator for queued items
    /// </summary>
    public IEnumerable<IUC_MFD> UserControls { get => m_list.AsEnumerable( ); }

    /// <summary>
    /// Get the muber of elements contained in the Queue
    /// </summary>
    public int Count { get => m_list.Count; }

    /// <summary>
    /// Add MFDs for use
    /// </summary>
    /// <param name="mfd">An MFD</param>
    public void Add( IUC_MFD mfd )
    {
      mfd.Visible = false;
      m_list.Add( mfd );
    }

    /// <summary>
    /// Get the next MFD from the queue
    /// </summary>
    /// <returns>The MFD in use or null if none are loaded</returns>
    public IUC_MFD GetNext()
    {
      if ( m_list.Count < 1 ) return null; // empty

      if ( m_currentIndex >= 0 ) m_list[m_currentIndex].Visible = false;
      m_currentIndex++;
      if ( m_currentIndex >= m_list.Count ) m_currentIndex = 0; // rollover
      var mfd = m_list.ElementAt( m_currentIndex );
      mfd.Visible = true;
      return mfd;
    }

    /// <summary>
    /// Get the previous MFD from the queue
    /// </summary>
    /// <returns>The MFD in use or null if none are loaded</returns>
    public IUC_MFD GetPrev()
    {
      if ( m_list.Count < 1 ) return null; // empty

      if ( m_currentIndex >= 0 ) m_list[m_currentIndex].Visible = false;
      m_currentIndex--;
      if ( m_currentIndex < 0 ) m_currentIndex = m_list.Count - 1; // rollunder
      var mfd = m_list.ElementAt( m_currentIndex );
      mfd.Visible = true;
      return mfd;
    }

  }
}
