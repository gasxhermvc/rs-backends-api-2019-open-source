using rs_backends_api_2019.Business.Models.Mock.Parameters.FirstPage;
using rs_backends_api_2019.DAL.Models.Defaults.RepositoryModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Interfaces.Mock
{
    public interface IFirstPageService
    {
        ResponseModel InterestingCategorys(InterestingCategorysParameter param);

        ResponseModel InterestingLandmark(InterestingLandmarkParameter param);

        ResponseModel NearbyAttactions(NearbyAttactionsParameter param);

        ResponseModel InterestingEvents(InterestingEventsParameter param);

        ResponseModel InterestingTips(InterestingTipsParameter param);
    }
}
